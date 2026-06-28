using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace DocsSamplesGenerator;

/// <summary>
/// Generates the DocsSamples documentation artifacts (rendered demo HTML, source-code includes, and
/// downloaded static assets) and writes them into the sibling <c>website</c> project.
/// </summary>
internal sealed partial class Generator
{
    private static readonly string RepoRootFolder = GetRepoRootFolder();

    // Generator.cs lives at <repoRoot>/docs/DocsSamplesGenerator/, so the repo root is two directories
    // up from this source file. [CallerFilePath] is resolved at compile time, which keeps this correct
    // regardless of the working directory the CLI is launched from.
    private static string GetRepoRootFolder([CallerFilePath] string sourceFilePath = "") =>
        Path.GetFullPath(Path.Combine(Path.GetDirectoryName(sourceFilePath)!, "..", ".."));

    /// <summary>Content root of the DocsSamples web project, used to host it in-memory.</summary>
    public static readonly string DocsSamplesContentRoot = Path.Combine(
        RepoRootFolder,
        "docs",
        "DocsSamples"
    );

    private static readonly string PageSourceCodeFolder = Path.Combine(
        RepoRootFolder,
        "docs",
        "DocsSamples",
        "Pages"
    );

    private static readonly string DocsProjectRootFolder = Environment.GetEnvironmentVariable(
        "DUNEUI_WEBSITE_DIR"
    )
        is { Length: > 0 } websiteDir
        ? Path.GetFullPath(websiteDir)
        : Path.GetFullPath(Path.Combine(RepoRootFolder, "..", "website"));

    private static readonly string RenderedPagesOutputFolder = Path.Combine(
        DocsProjectRootFolder,
        "public",
        "demo",
        "tag-helpers"
    );

    private static readonly string PagesSourceCodeOutputFolder = Path.Combine(
        DocsProjectRootFolder,
        "content",
        "docs",
        "tag-helpers",
        "components",
        "_include"
    );

    private static readonly string DownloadedAssetsOutputFolder = Path.Combine(
        DocsProjectRootFolder,
        "public",
        "demo",
        "tag-helpers",
        "assets"
    );

    /// <summary>Static assets referenced by demos at fixed, known URLs.</summary>
    public static readonly string[] FixedStaticAssets =
    [
        "/avatars/avatar-1.jpg",
        "/avatars/avatar-2.jpg",
        "/avatars/avatar-3.jpg",
        "/gradients/gradient-1.jpg",
    ];

    /// <summary>Downloads the demo assets that live at fixed, known URLs.</summary>
    public async Task DownloadFixedStaticAssetsAsync(HttpClient client)
    {
        if (!Directory.Exists(DownloadedAssetsOutputFolder))
            Directory.CreateDirectory(DownloadedAssetsOutputFolder);

        foreach (var url in FixedStaticAssets)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            await File.WriteAllBytesAsync(
                Path.Combine(DownloadedAssetsOutputFolder, Path.GetFileName(url)),
                await response.Content.ReadAsByteArrayAsync()
            );
        }
    }

    /// <summary>Scrapes the index page for CSS/JS assets and downloads them.</summary>
    public async Task DownloadDynamicStaticAssetsAsync(HttpClient client)
    {
        if (!Directory.Exists(DownloadedAssetsOutputFolder))
            Directory.CreateDirectory(DownloadedAssetsOutputFolder);

        var indexPageResponse = await client.GetAsync("/?clean");
        indexPageResponse.EnsureSuccessStatusCode();

        var content = await indexPageResponse.Content.ReadAsStringAsync();

        foreach (Match match in DemoAssetRegex().Matches(content))
        {
            var assetResponse = await client.GetAsync(match.Groups["url"].Value);
            assetResponse.EnsureSuccessStatusCode();

            await File.WriteAllBytesAsync(
                Path.Combine(
                    DownloadedAssetsOutputFolder,
                    Path.GetFileName(match.Groups["filename"].Value)
                ),
                await assetResponse.Content.ReadAsByteArrayAsync()
            );
        }
    }

    /// <summary>Reads a demo partial's source and writes a fenced Razor code-include (.mdx).</summary>
    public async Task GenerateDemoPartialSourceFileAsync(string page)
    {
        if (!Directory.Exists(PagesSourceCodeOutputFolder))
            Directory.CreateDirectory(PagesSourceCodeOutputFolder);

        var sourceFile = Path.Combine(
            PageSourceCodeFolder,
            page.Replace('/', Path.DirectorySeparatorChar) + ".cshtml"
        );

        var readSourceLines = await File.ReadAllLinesAsync(sourceFile);

        var stringsToRemove = new List<string>();
        var cleanedLines = new List<string>();
        var hasProcessedDirectives = false;
        var isProcessingStripSection = false;
        var charactersToDelete = 0;
        foreach (var sourceLine in readSourceLines)
        {
            // Read past the directives
            if (
                !hasProcessedDirectives
                && (sourceLine.StartsWith("@") || string.IsNullOrEmpty(sourceLine))
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

            var x =
                charactersToDelete > 0
                    ? sourceLine.Length > charactersToDelete
                        ? sourceLine.Remove(0, charactersToDelete)
                        : string.Empty
                    : sourceLine;

            foreach (var stringToRemove in stringsToRemove)
                x = x.Replace(stringToRemove, string.Empty, StringComparison.OrdinalIgnoreCase);
            cleanedLines.Add(x);
        }

        cleanedLines.Insert(0, "```razor");
        cleanedLines.Add("```");

        var filename = Path.Combine(PagesSourceCodeOutputFolder, GenerateFilename(page) + ".mdx");
        await File.WriteAllLinesAsync(filename, cleanedLines);
    }

    /// <summary>Renders a demo partial via the running site and writes the fixed-up HTML.</summary>
    public async Task RenderDemoPartialOutputAsync(HttpClient client, string partialName)
    {
        var response = await client.GetAsync($"/DocsStatic/?name={partialName}");

        var content = FixDemoContent(await response.Content.ReadAsStringAsync());

        if (!Directory.Exists(RenderedPagesOutputFolder))
            Directory.CreateDirectory(RenderedPagesOutputFolder);

        await File.WriteAllTextAsync(
            Path.Combine(RenderedPagesOutputFolder, $"{GenerateFilename(partialName)}.html"),
            content
        );
    }

    private static string FixDemoContent(string input)
    {
        // Fix the stylesheet references
        input = input
            .Replace(
                "src=\"/avatars/avatar-1.jpg\"",
                "src=\"/demo/tag-helpers/assets/avatar-1.jpg\""
            )
            .Replace(
                "src=\"/avatars/avatar-2.jpg\"",
                "src=\"/demo/tag-helpers/assets/avatar-2.jpg\""
            )
            .Replace(
                "src=\"/avatars/avatar-3.jpg\"",
                "src=\"/demo/tag-helpers/assets/avatar-3.jpg\""
            )
            .Replace(
                "src=\"/gradients/gradient-1.jpg\"",
                "src=\"/demo/tag-helpers/assets/gradient-1.jpg\""
            );

        input = DemoAssetRegex()
            .Replace(
                input,
                match =>
                    $"{match.Groups["tag"].Value}/demo/tag-helpers/assets/{match.Groups["filename"].Value}"
            );

        return input;
    }

    private static string GenerateFilename(string input)
    {
        // Simple kekab-case converter
        return Regex
            .Replace(
                input.Replace("/", "").Replace("_", ""),
                "(?!^)([A-Z])",
                "-$1",
                RegexOptions.Compiled
            )
            .Trim()
            .ToLower();
    }

    [GeneratedRegex(
        @"(?<tag><(link|script).*(href|src)="")(?<url>.*\/(?<filename>.*\.(css|js)))",
        RegexOptions.IgnoreCase,
        "en-US"
    )]
    private static partial Regex DemoAssetRegex();

    /// <summary>The demo partials to render and emit source includes for.</summary>
    public static readonly string[] DemoPartials =
    [
        "Accordion/_Disabled",
        "Accordion/_Intro",
        "Accordion/_Open",
        "Accordion/_Single",
        "Alert/_Actions",
        "Alert/_Basic",
        "Alert/_Destructive",
        "Alert/_Icons",
        "Alert/_Intro",
        "Alert/_Shorthand",
        "Avatar/_Badge",
        "Avatar/_BadgeWithIcon",
        "Avatar/_Group",
        "Avatar/_GroupWithCount",
        "Avatar/_GroupWithIconCount",
        "Avatar/_InEmpty",
        "Avatar/_Intro",
        "Avatar/_NameOrInitials",
        "Avatar/_Sizes",
        "Badge/_CustomColors",
        "Badge/_IconLeft",
        "Badge/_IconRight",
        "Badge/_Intro",
        "Badge/_LongText",
        "Badge/_Spinner",
        "Badge/_Variants",
        "Breadcrumb/_Collapsed",
        "Breadcrumb/_CustomCss",
        "Breadcrumb/_CustomSeparator",
        "Breadcrumb/_Icons",
        "Breadcrumb/_Intro",
        "Button/_AdditionalAttributes",
        "Button/_IconLeft",
        "Button/_IconOnly",
        "Button/_IconRight",
        "Button/_Intro",
        "Button/_InvalidStates",
        "Button/_SizesVariants",
        "Button/_Spinner",
        "ButtonGroup/_Basic",
        "ButtonGroup/_Intro",
        "ButtonGroup/_Nested",
        "ButtonGroup/_Pagination",
        "ButtonGroup/_PaginationSplit",
        "ButtonGroup/_Vertical",
        "ButtonGroup/_VerticalNested",
        "ButtonGroup/_Separator",
        "ButtonGroup/_Sizes",
        "ButtonGroup/_WithFields",
        "ButtonGroup/_WithIcons",
        "ButtonGroup/_WithInput",
        "ButtonGroup/_WithInputGroup",
        "ButtonGroup/_WithLike",
        "ButtonGroup/_WithPopover",
        "ButtonGroup/_WithSelect",
        "ButtonGroup/_WithSelectAndInput",
        "ButtonGroup/_WithText",
        "Card/_Default",
        "Card/_FooterWithBorder",
        "Card/_HeaderWithBorder",
        "Card/_Intro",
        "Card/_Login",
        "Card/_MeetingNotes",
        "Card/_Small",
        "Card/_WithImage",
        "Checkbox/_Disabled",
        "Checkbox/_FieldExplicit",
        "Checkbox/_FieldImplicit",
        "Checkbox/_Group",
        "Checkbox/_GroupModelBinding",
        "Checkbox/_Intro",
        "Checkbox/_ManualValidationExplicit",
        "Checkbox/_ManualValidationImplicit",
        "Checkbox/_ModelBinding",
        "Checkbox/_Validation",
        "Collapsible/_Intro",
        "Collapsible/_Settings",
        "Dialog/_Dismissing",
        "Dialog/_Intro",
        "Dialog/_JsApi",
        "Dialog/_JsEvents",
        "Dialog/_ReturnFormValue",
        "Dialog/_ReturnValue",
        "Dialog/_ScrollableContent",
        "Dialog/_StickyFooter",
        "Empty/_Basic",
        "Empty/_Intro",
        "Empty/_WithBorder",
        "Empty/_WithIcon",
        "Empty/_WithMutedBackground",
        "Empty/_WithMutedBackgroundAlt",
        "Field/_Checkbox",
        "Field/_CheckboxImplicit",
        "Field/_FieldGroup",
        "Field/_FieldGroupImplicit",
        "Field/_Fieldset",
        "Field/_FieldsetImplicit",
        "Field/_Implicit",
        "Field/_Input",
        "Field/_InputImplicit",
        "Field/_Intro",
        "Field/_Radio",
        "Field/_RadioImplicit",
        "Field/_Select",
        "Field/_SelectImplicit",
        "Field/_Textarea",
        "Field/_TextareaImplicit",
        "Field/_UsageField",
        "Field/_UsageFieldContent",
        "Field/_UsageFieldGroup",
        "Group/_Align",
        "Group/_Gap",
        "Group/_Justify",
        "Icon/_Color",
        "Icon/_Intro",
        "Icon/_Size",
        "Icon/_StrokeWidth",
        "Input/_FieldExplicit",
        "Input/_FieldImplicit",
        "Input/_InputTypesModelBinding",
        "Input/_InputTypes",
        "Input/_Intro",
        "Input/_ManualValidationExplicit",
        "Input/_ManualValidationImplicit",
        "Input/_ModelBinding",
        "Input/_Validation",
        "InputGroup/_ButtonGroup",
        "InputGroup/_Buttons",
        "InputGroup/_Icons",
        "InputGroup/_Intro",
        "InputGroup/_Label",
        "InputGroup/_Spinner",
        "InputGroup/_Text",
        "InputGroup/_Textarea",
        "Item/_Components",
        "Item/_Footer",
        "Item/_Group",
        "Item/_Header",
        "Item/_HeaderAndFooter",
        "Item/_Image",
        "Item/_Intro",
        "Item/_Link",
        "Item/_Separator",
        "Item/_Sizes",
        "Item/_Variants",
        "Js/Dialog/_FormValues",
        "Js/Dialog/_Intro",
        "Js/Dialog/_ManualResult",
        "Kbd/_ArrowKeys",
        "Kbd/_Basic",
        "Kbd/_InputGroup",
        "Kbd/_Intro",
        "Kbd/_KbdGroup",
        "Kbd/_ModifierKeys",
        "Kbd/_Tooltip",
        "Kbd/_WithIconAndText",
        "Kbd/_WithIcons",
        "LinkButton/_AdditionalAttributes",
        "LinkButton/_IconLeft",
        "LinkButton/_IconOnly",
        "LinkButton/_IconRight",
        "LinkButton/_Intro",
        "LinkButton/_SizesVariants",
        "LinkButton/_Url",
        "Pagination/_CustomContent",
        "Pagination/_Intro",
        "Pagination/_Url",
        "Popover/_ButtonGroup",
        "Popover/_Hover",
        "Popover/_Intro",
        "Popover/_JsApi",
        "Popover/_JsEvents",
        "Popover/_ManualDismiss",
        "Popover/_Offset",
        "Popover/_Position",
        "Progress/_FileUploadList",
        "Progress/_Intro",
        "Progress/_MinMax",
        "Progress/_WithLabel",
        "Radio/_Disabled",
        "Radio/_FieldExplicit",
        "Radio/_FieldImplicit",
        "Radio/_Intro",
        "Radio/_ManualValidationExplicit",
        "Radio/_ManualValidationImplicit",
        "Radio/_ModelBinding",
        "Radio/_Validation",
        "Select/_Disabled",
        "Select/_FieldExplicit",
        "Select/_FieldImplicit",
        "Select/_Groups",
        "Select/_Intro",
        "Select/_ManualValidationExplicit",
        "Select/_ManualValidationImplicit",
        "Select/_ModelBinding",
        "Select/_Sizes",
        "Select/_Validation",
        "Separator/_Horizontal",
        "Separator/_InList",
        "Separator/_Intro",
        "Separator/_Vertical",
        "Separator/_VerticalMenu",
        "Sidebar/_AlternateTriggerIcon",
        "Sidebar/_CollapsibleGroups",
        "Sidebar/_CollapsibleIcon",
        "Sidebar/_CollapsibleItems",
        "Sidebar/_FloatingVariant",
        "Sidebar/_InsetVariant",
        "Sidebar/_Intro",
        "Sidebar/_RightSide",
        "Sidebar/_SidebarInDialog",
        "Skeleton/_Avatar",
        "Skeleton/_Form",
        "Skeleton/_Intro",
        "Skeleton/_Table",
        "Skeleton/_Text",
        "Spinner/_Color",
        "Spinner/_InBadges",
        "Spinner/_InButtons",
        "Spinner/_InEmpty",
        "Spinner/_InInputGroup",
        "Spinner/_Intro",
        "Spinner/_Size",
        "Stack/_Align",
        "Stack/_Gap",
        "Stack/_Justify",
        "Table/_Border",
        "Table/_Intro",
        "Table/_Select",
        "Tabs/_Active",
        "Tabs/_Disabled",
        "Tabs/_Icons",
        "Tabs/_IconsOnly",
        "Tabs/_Intro",
        "Tabs/_Line",
        "Tabs/_Orientation",
        "Tabs/_Url",
        "Textarea/_FieldExplicit",
        "Textarea/_FieldImplicit",
        "Textarea/_Intro",
        "Textarea/_ManualValidationExplicit",
        "Textarea/_ManualValidationImplicit",
        "Textarea/_ModelBinding",
        "Textarea/_Validation",
        "Tooltip/_Delay",
        "Tooltip/_Elements",
        "Tooltip/_Intro",
        "Tooltip/_JsApi",
        "Tooltip/_JsEvents",
        "Tooltip/_Offset",
        "Tooltip/_Position",
    ];
}
