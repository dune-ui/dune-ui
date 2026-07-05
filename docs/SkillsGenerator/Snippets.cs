namespace SkillsGenerator;

/// <summary>
/// Extracts the usage snippet from a docs sample partial. This is a faithful port of
/// <c>DocsSamplesGenerator.Generator.GenerateDemoPartialSourceFileAsync</c>: skip leading
/// <c>@</c>-directives and blank lines, drop <c>&lt;!--strip ... --&gt;</c> blocks, begin capturing at
/// <c>&lt;!-- code begin --&gt;</c> (dedenting by the marker column), and stop at <c>&lt;!-- code end --&gt;</c>.
/// </summary>
internal static class Snippets
{
    private static readonly Dictionary<string, string> DemoFolderAlias = new(StringComparer.Ordinal)
    {
        ["CheckboxGroup"] = "Checkbox",
        ["RadioGroup"] = "Radio",
        ["Layout"] = "Group",
    };

    /// <summary>
    /// Chooses the snippet partial for a component folder and extracts it. Returns the inner razor
    /// text (no fences) and the repo-relative source label (e.g. <c>Pages/Button/_Intro.cshtml</c>),
    /// or (null, null) when the component has no example.
    /// </summary>
    public static (string? Snippet, string? Source) ForComponent(
        string pagesRoot,
        string folderName
    )
    {
        var demoFolder = DemoFolderAlias.GetValueOrDefault(folderName, folderName);
        var folder = Path.Combine(pagesRoot, demoFolder);
        if (!Directory.Exists(folder))
            return (null, null);

        var intro = Path.Combine(folder, "_Intro.cshtml");
        string? chosen = File.Exists(intro)
            ? intro
            : Directory
                .GetFiles(folder, "_*.cshtml")
                .OrderBy(Path.GetFileName, StringComparer.Ordinal)
                .FirstOrDefault();

        if (chosen is null)
            return (null, null);

        var snippet = Extract(File.ReadAllLines(chosen));
        var source = $"Pages/{demoFolder}/{Path.GetFileName(chosen)}";
        return (snippet, source);
    }

    private static string Extract(string[] readSourceLines)
    {
        var stringsToRemove = new List<string>();
        var cleanedLines = new List<string>();
        var hasProcessedDirectives = false;
        var isProcessingStripSection = false;
        var charactersToDelete = 0;

        foreach (var sourceLine in readSourceLines)
        {
            if (
                !hasProcessedDirectives
                && (sourceLine.StartsWith('@') || string.IsNullOrEmpty(sourceLine))
            )
                continue;

            if (isProcessingStripSection)
            {
                if (sourceLine.StartsWith("-->", StringComparison.CurrentCultureIgnoreCase))
                    isProcessingStripSection = false;
                else
                    stringsToRemove.Add(sourceLine);

                continue;
            }

            if (sourceLine.StartsWith("<!--strip", StringComparison.CurrentCultureIgnoreCase))
            {
                isProcessingStripSection = true;
                continue;
            }

            hasProcessedDirectives = true;

            if (sourceLine.IndexOf("<!-- code end -->", StringComparison.Ordinal) >= 0)
                break;

            if (
                sourceLine.IndexOf("<!-- code begin -->", StringComparison.Ordinal)
                is var index
                    and >= 0
            )
            {
                charactersToDelete = index;
                cleanedLines.Clear();
                continue;
            }

            var line =
                charactersToDelete > 0
                    ? sourceLine.Length > charactersToDelete
                        ? sourceLine.Remove(0, charactersToDelete)
                        : string.Empty
                    : sourceLine;

            foreach (var stringToRemove in stringsToRemove)
                line = line.Replace(
                    stringToRemove,
                    string.Empty,
                    StringComparison.OrdinalIgnoreCase
                );

            cleanedLines.Add(line);
        }

        return string.Join("\n", cleanedLines).TrimEnd('\n');
    }
}
