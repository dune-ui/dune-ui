using System.Text;

namespace SkillsGenerator;

/// <summary>
/// Renders <see cref="ComponentInfo"/> models into the per-component reference markdown and the
/// component index. Output is deterministic and byte-stable across runs.
/// </summary>
internal sealed class Renderer(Dictionary<string, EnumInfo> enums)
{
    private const string StructureBegin = "<!-- structure:begin -->";
    private const string StructureEnd = "<!-- structure:end -->";

    private const string ClassRow =
        "| `class` | `string` | — | Extra Tailwind utilities; merged last, so it overrides defaults. |";

    private const string EnumNote =
        "> In Razor, enum values are written fully-qualified, e.g. `variant=\"ButtonVariant.Outline\"`.";

    /// <summary>
    /// Renders the markdown for a component. <paramref name="existingContent"/> is the current
    /// on-disk file (or null); its preserved structure region is carried over verbatim.
    /// </summary>
    public string RenderComponent(ComponentInfo component, string? existingContent)
    {
        var parts = new List<string> { Frontmatter(component), "# " + component.FolderName };

        if (!string.IsNullOrEmpty(component.Primary.Summary))
            parts.Add(component.Primary.Summary);

        // Only carry over a hand-authored structure region if one exists with real content;
        // simple/flat components (e.g. Switch) get no empty markers.
        var structure = PreservedStructureRegion(existingContent);
        if (structure is not null)
            parts.Add(structure);

        if (component.Tags.Count > 1)
            parts.Add(TagsTable(component));

        var attributes = AttributesSection(component);
        if (attributes is not null)
            parts.Add(attributes);

        if (component.Snippet is not null)
            parts.Add(ExampleSection(component));

        return string.Join("\n\n", parts) + "\n";
    }

    private static string Frontmatter(ComponentInfo component)
    {
        var tags = string.Join(", ", component.Tags.Select(t => t.Name));
        return $"---\ncomponent: {component.FolderName}\ntags: [{tags}]\ngenerated: true\n---";
    }

    /// <summary>
    /// Returns the full hand-authored structure region (markers + body) from the existing file if
    /// one is present with non-whitespace content, otherwise null. This is how the "Required
    /// structure" trees on composite components (Sidebar, Sheet) are preserved across regeneration
    /// while flat components carry no empty markers.
    /// </summary>
    private static string? PreservedStructureRegion(string? existingContent)
    {
        if (string.IsNullOrEmpty(existingContent))
            return null;

        var start = existingContent.IndexOf(StructureBegin, StringComparison.Ordinal);
        if (start < 0)
            return null;

        var bodyStart = start + StructureBegin.Length;
        var end = existingContent.IndexOf(StructureEnd, bodyStart, StringComparison.Ordinal);
        if (end < 0)
            return null;

        var body = existingContent[bodyStart..end];
        if (string.IsNullOrWhiteSpace(body))
            return null;

        return StructureBegin + body + StructureEnd;
    }

    private static string TagsTable(ComponentInfo component)
    {
        var builder = new StringBuilder();
        builder.Append("## Tags\n\n");
        builder.Append("| Tag | Description |\n");
        builder.Append("|-----|-------------|\n");

        foreach (var tag in component.Tags)
            builder.Append($"| `<{tag.Name}>` | {DashIfEmpty(tag.Summary)} |\n");

        return builder.ToString().TrimEnd('\n');
    }

    private string? AttributesSection(ComponentInfo component)
    {
        var hasEnumAttribute = component.Tags.Any(t =>
            t.Attributes.Any(a => enums.ContainsKey(StripNullable(a.TypeText)))
        );

        var builder = new StringBuilder();
        builder.Append("## Attributes\n");

        var noteWritten = false;

        if (component.Tags.Count == 1)
        {
            builder.Append('\n');
            builder.Append(AttributeTable(component.Tags[0].Attributes));
            if (hasEnumAttribute)
                builder.Append("\n\n" + EnumNote);
            return builder.ToString().TrimEnd('\n');
        }

        var tagsWithAttributes = component.Tags.Where(t => t.Attributes.Count > 0).ToList();
        if (tagsWithAttributes.Count == 0)
            return null;

        foreach (var tag in tagsWithAttributes)
        {
            builder.Append($"\n### `<{tag.Name}>`\n\n");
            builder.Append(AttributeTable(tag.Attributes));
            builder.Append('\n');

            if (hasEnumAttribute && !noteWritten)
            {
                builder.Append("\n" + EnumNote + "\n");
                noteWritten = true;
            }
        }

        return builder.ToString().TrimEnd('\n');
    }

    private string AttributeTable(IReadOnlyList<AttributeInfo> attributes)
    {
        var builder = new StringBuilder();
        builder.Append("| Attribute | Type | Default | Values |\n");
        builder.Append("|-----------|------|---------|--------|\n");

        foreach (var attribute in attributes)
        {
            var (type, values) = TypeAndValues(attribute.TypeText);
            var def = attribute.Default == "—" ? "—" : $"`{attribute.Default}`";
            builder.Append($"| `{attribute.Name}` | {type} | {def} | {values} |\n");
        }

        builder.Append(ClassRow + "\n");
        return builder.ToString().TrimEnd('\n');
    }

    private (string Type, string Values) TypeAndValues(string typeText)
    {
        var stripped = StripNullable(typeText);

        if (stripped == "bool")
            return ("`bool`", "`true`, `false`");

        if (enums.TryGetValue(stripped, out var enumInfo))
        {
            var values = string.Join(", ", enumInfo.Members.Select(m => $"`{m.Name}`"));
            return ($"`{stripped}`", values);
        }

        return ($"`{stripped}`", "—");
    }

    private static string ExampleSection(ComponentInfo component)
    {
        var builder = new StringBuilder();
        builder.Append("## Example\n\n");
        builder.Append($"*From `{component.SnippetSource}`*\n\n");
        builder.Append("```razor\n");
        builder.Append(component.Snippet);
        builder.Append("\n```");
        return builder.ToString();
    }

    /// <summary>Renders the component catalog index.</summary>
    public static string RenderIndex(IReadOnlyList<ComponentInfo> components)
    {
        var builder = new StringBuilder();
        builder.Append("# DuneUI component catalog\n\n");
        builder.Append("| Component | Tags | Summary |\n");
        builder.Append("|-----------|------|---------|\n");

        foreach (var component in components.OrderBy(c => c.FolderName, StringComparer.Ordinal))
        {
            var link =
                $"[{component.FolderName}](components/{Extractor.ToKebabCase(component.FolderName)}.md)";
            var tags = TagsColumn(component.Tags);
            var summary = FirstSentence(component.Primary.Summary);
            builder.Append($"| {link} | {tags} | {summary} |\n");
        }

        return builder.ToString().TrimEnd('\n') + "\n";
    }

    private static string TagsColumn(IReadOnlyList<TagInfo> tags)
    {
        var names = tags.Select(t => $"`<{t.Name}>`").ToList();
        if (names.Count > 3)
            return string.Join(", ", names.Take(3)) + ", …";

        return string.Join(", ", names);
    }

    private static string FirstSentence(string summary)
    {
        if (string.IsNullOrEmpty(summary))
            return "—";

        var idx = summary.IndexOf(". ", StringComparison.Ordinal);
        return idx < 0 ? summary : summary[..(idx + 1)];
    }

    private static string StripNullable(string typeText) =>
        typeText.EndsWith('?') ? typeText[..^1] : typeText;

    private static string DashIfEmpty(string value) => string.IsNullOrEmpty(value) ? "—" : value;
}
