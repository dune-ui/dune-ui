using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SkillsGenerator;

/// <summary>
/// Extracts <c>&lt;summary&gt;</c> text and a "default value" hint from a declaration's XML
/// doc-comment. Purely textual — no semantic model. Never invents prose.
/// </summary>
internal static partial class DocComments
{
    [GeneratedRegex(@"<summary>(.*?)</summary>", RegexOptions.Singleline)]
    private static partial Regex SummaryRegex();

    [GeneratedRegex(@"<remarks>(.*?)</remarks>", RegexOptions.Singleline)]
    private static partial Regex RemarksRegex();

    [GeneratedRegex("cref=\"[^\"]*?\\.([A-Za-z0-9_]+)\"")]
    private static partial Regex CrefRegex();

    [GeneratedRegex(@"<c>([^<]*)</c>")]
    private static partial Regex CodeRegex();

    [GeneratedRegex(@"<c>(.*?)</c>", RegexOptions.Singleline)]
    private static partial Regex CodeTagRegex();

    [GeneratedRegex(@"<see\s+cref=""(?:[A-Za-z]:)?([^""]+)""\s*/>")]
    private static partial Regex SeeCrefRegex();

    [GeneratedRegex(@"<paramref\s+name=""([^""]+)""\s*/>")]
    private static partial Regex ParamRefRegex();

    /// <summary>Returns the raw doc-comment text (with <c>///</c> prefixes) for a node, or empty.</summary>
    private static string RawDocComment(SyntaxNode node) =>
        node.GetLeadingTrivia()
            .Select(t => t.GetStructure())
            .OfType<DocumentationCommentTriviaSyntax>()
            .FirstOrDefault()
            ?.ToFullString()
        ?? string.Empty;

    /// <summary>The collapsed <c>&lt;summary&gt;</c> text for a declaration, or empty string.</summary>
    public static string Summary(SyntaxNode node)
    {
        var doc = RawDocComment(node);
        var match = SummaryRegex().Match(doc);
        if (!match.Success)
            return string.Empty;

        return Collapse(InlineToMarkdown(StripTripleSlash(match.Groups[1].Value)));
    }

    /// <summary>
    /// Converts the inline XML doc tags we use (<c>&lt;c&gt;</c>, <c>&lt;see cref&gt;</c>,
    /// <c>&lt;paramref&gt;</c>) to their Markdown equivalents and decodes XML entities, so summary
    /// prose renders correctly in the generated reference (e.g. <c>&lt;c&gt;&amp;lt;dialog&amp;gt;&lt;/c&gt;</c>
    /// becomes a backticked <c>&lt;dialog&gt;</c>). This is faithful rendering, not invented prose.
    /// </summary>
    private static string InlineToMarkdown(string value)
    {
        value = CodeTagRegex().Replace(value, "`$1`");
        value = SeeCrefRegex()
            .Replace(
                value,
                m =>
                {
                    var target = m.Groups[1].Value;
                    var lastDot = target.LastIndexOf('.');
                    return "`" + (lastDot >= 0 ? target[(lastDot + 1)..] : target) + "`";
                }
            );
        value = ParamRefRegex().Replace(value, "`$1`");

        // Decode entities last (&amp; last so we don't double-decode).
        return value
            .Replace("&lt;", "<")
            .Replace("&gt;", ">")
            .Replace("&quot;", "\"")
            .Replace("&#39;", "'")
            .Replace("&amp;", "&");
    }

    /// <summary>
    /// The default-value hint mined from <c>&lt;remarks&gt;</c>: the last segment of the first
    /// <c>cref</c>, else the inner text of the first <c>&lt;c&gt;</c>. Returns "—" when absent.
    /// </summary>
    public static string Default(SyntaxNode node)
    {
        var doc = RawDocComment(node);
        var remarks = RemarksRegex().Match(doc);
        if (!remarks.Success)
            return "—";

        var body = remarks.Groups[1].Value;

        var cref = CrefRegex().Match(body);
        if (cref.Success)
            return cref.Groups[1].Value;

        var code = CodeRegex().Match(body);
        if (code.Success)
            return Collapse(code.Groups[1].Value);

        return "—";
    }

    private static string StripTripleSlash(string value)
    {
        var lines = value.Replace("\r\n", "\n").Split('\n');
        return string.Join(
            "\n",
            lines.Select(line =>
            {
                var trimmed = line.TrimStart();
                return trimmed.StartsWith("///", StringComparison.Ordinal) ? trimmed[3..] : line;
            })
        );
    }

    /// <summary>Collapses all runs of whitespace (incl. newlines) to a single space and trims.</summary>
    private static string Collapse(string value) => Regex.Replace(value, @"\s+", " ").Trim();
}
