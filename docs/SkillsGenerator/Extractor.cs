using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SkillsGenerator;

/// <summary>
/// Parses the tag-helper C# sources into <see cref="ComponentInfo"/> models using Roslyn syntax
/// trees only (no compilation / semantic model). Deterministic: all iteration is explicitly ordered.
/// </summary>
internal static partial class Extractor
{
    /// <summary>
    /// Builds the global enum dictionary (enum name -> ordered members) by scanning EVERY .cs file
    /// under the tag-helpers root, so cross-folder enum references (e.g. a DropdownMenu attribute
    /// typed as <c>ButtonSize</c> or <c>PositionArea</c>) resolve.
    /// </summary>
    public static Dictionary<string, EnumInfo> BuildEnumIndex(string tagHelpersRoot)
    {
        var result = new Dictionary<string, EnumInfo>(StringComparer.Ordinal);

        foreach (var file in EnumerateCsFiles(tagHelpersRoot))
        {
            var root = ParseFile(file);
            foreach (var enumDecl in root.DescendantNodes().OfType<EnumDeclarationSyntax>())
            {
                if (!IsPublic(enumDecl.Modifiers))
                    continue;

                var members = enumDecl
                    .Members.Select(m => new EnumMember(m.Identifier.Text, DocComments.Summary(m)))
                    .ToList();

                result[enumDecl.Identifier.Text] = new EnumInfo(enumDecl.Identifier.Text, members);
            }
        }

        return result;
    }

    /// <summary>
    /// Discovers every component folder (a direct subfolder of <paramref name="tagHelpersRoot"/>
    /// containing at least one <c>[HtmlTargetElement("...")]</c> class) and extracts its tags.
    /// Returned sorted by folder name.
    /// </summary>
    public static List<ComponentInfo> ExtractComponents(
        string tagHelpersRoot,
        Dictionary<string, EnumInfo> enums,
        Func<string, (string? Snippet, string? Source)> snippetLookup
    )
    {
        var components = new List<ComponentInfo>();

        var folders = Directory
            .GetDirectories(tagHelpersRoot)
            .OrderBy(Path.GetFileName, StringComparer.Ordinal);

        foreach (var folder in folders)
        {
            var folderName = Path.GetFileName(folder);
            var tags = ExtractTagsForFolder(folder, enums);
            if (tags.Count == 0)
                continue; // No [HtmlTargetElement] -> not a component (e.g. Menu/).

            var ordered = OrderTags(tags, out var primary);
            var (snippet, source) = snippetLookup(folderName);

            components.Add(new ComponentInfo(folderName, ordered, primary, snippet, source));
        }

        return components;
    }

    private static List<TagInfo> ExtractTagsForFolder(
        string folder,
        Dictionary<string, EnumInfo> enums
    )
    {
        var tags = new List<TagInfo>();

        // Sort files by name so the "source order" of tags/classes is deterministic across runs.
        var files = Directory
            .GetFiles(folder, "*.cs", SearchOption.AllDirectories)
            .OrderBy(Path.GetFileName, StringComparer.Ordinal);

        foreach (var file in files)
        {
            var root = ParseFile(file);
            foreach (var classDecl in root.DescendantNodes().OfType<ClassDeclarationSyntax>())
            {
                var tagNames = TargetElementNames(classDecl);
                if (tagNames.Count == 0)
                    continue;

                var summary = DocComments.Summary(classDecl);
                var attributes = ExtractAttributes(classDecl, enums);

                foreach (var tagName in tagNames)
                    tags.Add(new TagInfo(tagName, summary, attributes));
            }
        }

        return tags;
    }

    /// <summary>The tag names from every <c>[HtmlTargetElement("...")]</c> with a string-literal arg.</summary>
    private static List<string> TargetElementNames(ClassDeclarationSyntax classDecl)
    {
        var names = new List<string>();

        foreach (var attribute in classDecl.AttributeLists.SelectMany(a => a.Attributes))
        {
            var name = attribute.Name.ToString();
            if (name is not ("HtmlTargetElement" or "HtmlTargetElementAttribute"))
                continue;

            var firstArg = attribute.ArgumentList?.Arguments.FirstOrDefault();
            if (
                firstArg?.Expression is LiteralExpressionSyntax literal
                && literal.IsKind(SyntaxKind.StringLiteralExpression)
            )
            {
                names.Add(literal.Token.ValueText);
            }
        }

        return names;
    }

    private static List<AttributeInfo> ExtractAttributes(
        ClassDeclarationSyntax classDecl,
        Dictionary<string, EnumInfo> enums
    )
    {
        var attributes = new List<AttributeInfo>();

        foreach (var prop in classDecl.Members.OfType<PropertyDeclarationSyntax>())
        {
            if (!IsPublic(prop.Modifiers))
                continue;
            if (prop.Modifiers.Any(SyntaxKind.StaticKeyword))
                continue;
            if (!HasSetter(prop))
                continue;
            if (HasAttribute(prop, "HtmlAttributeNotBound"))
                continue;

            var name = AttributeName(prop);
            var typeText = prop.Type.ToString();

            attributes.Add(
                new AttributeInfo(
                    name,
                    typeText,
                    DocComments.Summary(prop),
                    DocComments.Default(prop)
                )
            );
        }

        return attributes;
    }

    private static string AttributeName(PropertyDeclarationSyntax prop)
    {
        foreach (var attribute in prop.AttributeLists.SelectMany(a => a.Attributes))
        {
            if (
                attribute.Name.ToString()
                is not ("HtmlAttributeName" or "HtmlAttributeNameAttribute")
            )
                continue;

            var firstArg = attribute.ArgumentList?.Arguments.FirstOrDefault();
            if (
                firstArg?.Expression is LiteralExpressionSyntax literal
                && literal.IsKind(SyntaxKind.StringLiteralExpression)
            )
            {
                return literal.Token.ValueText;
            }
        }

        return ToKebabCase(prop.Identifier.Text);
    }

    private static bool HasSetter(PropertyDeclarationSyntax prop)
    {
        var accessors = prop.AccessorList?.Accessors;
        if (accessors is null)
            return false; // expression-bodied get-only property

        return accessors.Value.Any(a =>
            a.IsKind(SyntaxKind.SetAccessorDeclaration)
            || a.IsKind(SyntaxKind.InitAccessorDeclaration)
        );
    }

    private static bool HasAttribute(PropertyDeclarationSyntax prop, string attributeName) =>
        prop
            .AttributeLists.SelectMany(a => a.Attributes)
            .Any(a =>
                a.Name.ToString() == attributeName
                || a.Name.ToString() == attributeName + "Attribute"
            );

    private static bool IsPublic(SyntaxTokenList modifiers) =>
        modifiers.Any(SyntaxKind.PublicKeyword);

    /// <summary>
    /// Orders tags for output: the primary tag first, then the rest alphabetically. The primary is
    /// the shortest tag whose name is a prefix of every other tag; failing that, the first declared.
    /// </summary>
    private static List<TagInfo> OrderTags(List<TagInfo> tags, out TagInfo primary)
    {
        var chosen = ChoosePrimary(tags);
        primary = chosen;

        var rest = tags.Where(t => t.Name != chosen.Name)
            .OrderBy(t => t.Name, StringComparer.Ordinal)
            .ToList();

        var ordered = new List<TagInfo>(tags.Count) { chosen };
        ordered.AddRange(rest);
        return ordered;
    }

    private static TagInfo ChoosePrimary(List<TagInfo> tags)
    {
        if (tags.Count == 1)
            return tags[0];

        // The root tag is the one that is a prefix of the most other tags (e.g. dui-item for the
        // Item family, dui-sidebar for Sidebar). Ties break toward the shortest, then alphabetical.
        // This is robust to outlier tags (e.g. dui-link-item) that don't share the family prefix.
        return tags.OrderByDescending(t =>
                tags.Count(o =>
                    o.Name != t.Name && o.Name.StartsWith(t.Name + "-", StringComparison.Ordinal)
                )
            )
            .ThenBy(t => t.Name.Length)
            .ThenBy(t => t.Name, StringComparer.Ordinal)
            .First();
    }

    private static IEnumerable<string> EnumerateCsFiles(string root) =>
        Directory
            .GetFiles(root, "*.cs", SearchOption.AllDirectories)
            .OrderBy(f => f, StringComparer.Ordinal);

    private static CompilationUnitSyntax ParseFile(string path) =>
        (CompilationUnitSyntax)CSharpSyntaxTree.ParseText(File.ReadAllText(path)).GetRoot();

    [GeneratedRegex("(?<!^)([A-Z])")]
    private static partial Regex KebabRegex();

    public static string ToKebabCase(string value) =>
        KebabRegex().Replace(value, "-$1").ToLowerInvariant();
}
