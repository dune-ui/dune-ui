using System.Text.Json;

namespace SkillsGenerator;

/// <summary>
/// Extracts usage snippets from docs sample partials. The snippet extraction is a faithful port of
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
    /// Loads the curated example manifest (component folder -> ordered demo-partial paths). The
    /// leading <c>$comment</c> property and any non-array values are ignored, so the file can carry
    /// documentation. Returns an empty map when the manifest is absent.
    /// </summary>
    public static Dictionary<string, string[]> LoadManifest(string path)
    {
        var result = new Dictionary<string, string[]>(StringComparer.Ordinal);
        if (!File.Exists(path))
            return result;

        using var document = JsonDocument.Parse(File.ReadAllText(path));
        foreach (var property in document.RootElement.EnumerateObject())
        {
            if (property.Value.ValueKind != JsonValueKind.Array)
                continue;

            result[property.Name] = property
                .Value.EnumerateArray()
                .Select(item => item.GetString()!)
                .ToArray();
        }

        return result;
    }

    /// <summary>
    /// Resolves the ordered examples for a component folder. When the manifest lists the folder, each
    /// listed demo-partial path is extracted (missing files are skipped). Otherwise it falls back to
    /// a single example: the folder's <c>_Intro</c> partial, or the first partial alphabetically.
    /// </summary>
    public static IReadOnlyList<ExampleInfo> ForComponent(
        string pagesRoot,
        string folderName,
        Dictionary<string, string[]> manifest
    )
    {
        if (manifest.TryGetValue(folderName, out var paths))
        {
            var examples = new List<ExampleInfo>();
            foreach (var path in paths)
            {
                var file = Path.Combine(
                    pagesRoot,
                    path.Replace('/', Path.DirectorySeparatorChar) + ".cshtml"
                );
                if (!File.Exists(file))
                    continue;

                examples.Add(
                    new ExampleInfo(Extract(File.ReadAllLines(file)), $"Pages/{path}.cshtml")
                );
            }

            return examples;
        }

        var demoFolder = DemoFolderAlias.GetValueOrDefault(folderName, folderName);
        var folder = Path.Combine(pagesRoot, demoFolder);
        if (!Directory.Exists(folder))
            return [];

        var intro = Path.Combine(folder, "_Intro.cshtml");
        string? chosen = File.Exists(intro)
            ? intro
            : Directory
                .GetFiles(folder, "_*.cshtml")
                .OrderBy(Path.GetFileName, StringComparer.Ordinal)
                .FirstOrDefault();

        if (chosen is null)
            return [];

        return
        [
            new ExampleInfo(
                Extract(File.ReadAllLines(chosen)),
                $"Pages/{demoFolder}/{Path.GetFileName(chosen)}"
            ),
        ];
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
