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

    /// <summary>
    /// Deletes previously generated output so renamed/removed demos don't leave orphaned files behind.
    /// Only the two top-level output trees are cleared; the assets folder is nested inside
    /// <see cref="RenderedPagesOutputFolder"/> and is removed along with it.
    /// </summary>
    public void CleanOutputFolders()
    {
        foreach (var folder in new[] { RenderedPagesOutputFolder, PagesSourceCodeOutputFolder })
        {
            if (Directory.Exists(folder))
                Directory.Delete(folder, recursive: true);
        }
    }

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
    public async Task RenderDemoPartialOutputAsync(
        HttpClient client,
        string partialName,
        string? layout
    )
    {
        var response = await client.GetAsync($"/DocsStatic/?name={partialName}&layout={layout}");

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
    public static readonly DemoPartial[] DemoPartials =
    [
        new("Accordion/_Disabled"),
        new("Accordion/_Intro"),
        new("Accordion/_Open"),
        new("Accordion/_Single"),
        new("Alert/_Actions"),
        new("Alert/_Basic"),
        new("Alert/_Destructive"),
        new("Alert/_Icons"),
        new("Alert/_Intro"),
        new("Alert/_Shorthand"),
        new("AlertDialog/_Destructive"),
        new("AlertDialog/_Intro"),
        new("AlertDialog/_JsApi"),
        new("AlertDialog/_Media"),
        new("AlertDialog/_Size"),
        new("AlertDialog/_SmallMedia"),
        new("Avatar/_Badge"),
        new("Avatar/_BadgeWithIcon"),
        new("Avatar/_Group"),
        new("Avatar/_GroupWithCount"),
        new("Avatar/_GroupWithIconCount"),
        new("Avatar/_InEmpty"),
        new("Avatar/_Intro"),
        new("Avatar/_NameOrInitials"),
        new("Avatar/_Sizes"),
        new("Badge/_CustomColors"),
        new("Badge/_IconLeft"),
        new("Badge/_IconRight"),
        new("Badge/_Intro"),
        new("Badge/_LongText"),
        new("Badge/_Spinner"),
        new("Badge/_Variants"),
        new("Breadcrumb/_Collapsed"),
        new("Breadcrumb/_CustomCss"),
        new("Breadcrumb/_CustomSeparator"),
        new("Breadcrumb/_Icons"),
        new("Breadcrumb/_Intro"),
        new("Button/_AdditionalAttributes"),
        new("Button/_IconLeft"),
        new("Button/_IconOnly"),
        new("Button/_IconRight"),
        new("Button/_Intro"),
        new("Button/_InvalidStates"),
        new("Button/_SizesVariants"),
        new("Button/_Spinner"),
        new("ButtonGroup/_Basic"),
        new("ButtonGroup/_Intro"),
        new("ButtonGroup/_Nested"),
        new("ButtonGroup/_Pagination"),
        new("ButtonGroup/_PaginationSplit"),
        new("ButtonGroup/_Vertical"),
        new("ButtonGroup/_VerticalNested"),
        new("ButtonGroup/_Separator"),
        new("ButtonGroup/_Sizes"),
        new("ButtonGroup/_WithFields"),
        new("ButtonGroup/_WithIcons"),
        new("ButtonGroup/_WithInput"),
        new("ButtonGroup/_WithInputGroup"),
        new("ButtonGroup/_WithLike"),
        new("ButtonGroup/_WithPopover"),
        new("ButtonGroup/_WithSelect"),
        new("ButtonGroup/_WithSelectAndInput"),
        new("ButtonGroup/_WithText"),
        new("Card/_Default"),
        new("Card/_FooterWithBorder"),
        new("Card/_HeaderWithBorder"),
        new("Card/_Intro"),
        new("Card/_Login"),
        new("Card/_MeetingNotes"),
        new("Card/_Small"),
        new("Card/_WithImage"),
        new("Checkbox/_Disabled"),
        new("Checkbox/_FieldExplicit"),
        new("Checkbox/_FieldImplicit"),
        new("Checkbox/_Group"),
        new("Checkbox/_GroupModelBinding"),
        new("Checkbox/_Intro"),
        new("Checkbox/_ManualValidationExplicit"),
        new("Checkbox/_ManualValidationImplicit"),
        new("Checkbox/_ModelBinding"),
        new("Checkbox/_Validation"),
        new("Collapsible/_Intro"),
        new("Collapsible/_Settings"),
        new("Dialog/_Dismissing"),
        new("Dialog/_Intro"),
        new("Dialog/_JsApi"),
        new("Dialog/_JsEvents"),
        new("Dialog/_ReturnFormValue"),
        new("Dialog/_ReturnValue"),
        new("Dialog/_ScrollableContent"),
        new("Dialog/_StickyFooter"),
        new("Empty/_Basic"),
        new("Empty/_Intro"),
        new("Empty/_WithBorder"),
        new("Empty/_WithIcon"),
        new("Empty/_WithMutedBackground"),
        new("Empty/_WithMutedBackgroundAlt"),
        new("Field/_Checkbox"),
        new("Field/_CheckboxImplicit"),
        new("Field/_FieldGroup"),
        new("Field/_FieldGroupImplicit"),
        new("Field/_Fieldset"),
        new("Field/_FieldsetImplicit"),
        new("Field/_Implicit"),
        new("Field/_Input"),
        new("Field/_InputImplicit"),
        new("Field/_Intro"),
        new("Field/_Radio"),
        new("Field/_RadioImplicit"),
        new("Field/_Select"),
        new("Field/_SelectImplicit"),
        new("Field/_Textarea"),
        new("Field/_TextareaImplicit"),
        new("Field/_UsageField"),
        new("Field/_UsageFieldContent"),
        new("Field/_UsageFieldGroup"),
        new("Group/_Align"),
        new("Group/_Gap"),
        new("Group/_Justify"),
        new("Icon/_Color"),
        new("Icon/_Intro"),
        new("Icon/_Size"),
        new("Icon/_StrokeWidth"),
        new("Input/_FieldExplicit"),
        new("Input/_FieldImplicit"),
        new("Input/_InputTypesModelBinding"),
        new("Input/_InputTypes"),
        new("Input/_Intro"),
        new("Input/_ManualValidationExplicit"),
        new("Input/_ManualValidationImplicit"),
        new("Input/_ModelBinding"),
        new("Input/_Validation"),
        new("InputGroup/_ButtonGroup"),
        new("InputGroup/_Buttons"),
        new("InputGroup/_Icons"),
        new("InputGroup/_Intro"),
        new("InputGroup/_Label"),
        new("InputGroup/_Spinner"),
        new("InputGroup/_Text"),
        new("InputGroup/_Textarea"),
        new("InputOtp/_Composition"),
        new("InputOtp/_CustomSeparator"),
        new("InputOtp/_Disabled"),
        new("InputOtp/_FourDigits"),
        new("InputOtp/_Groups"),
        new("InputOtp/_Intro"),
        new("InputOtp/_ManualValidationExplicit"),
        new("InputOtp/_ManualValidationImplicit"),
        new("InputOtp/_ModelBinding"),
        new("InputOtp/_Pattern"),
        new("InputOtp/_Separator"),
        new("InputOtp/_Size"),
        new("InputOtp/_Validation"),
        new("Item/_Components"),
        new("Item/_Footer"),
        new("Item/_Group"),
        new("Item/_Header"),
        new("Item/_HeaderAndFooter"),
        new("Item/_Image"),
        new("Item/_Intro"),
        new("Item/_Link"),
        new("Item/_Separator"),
        new("Item/_Sizes"),
        new("Item/_Variants"),
        new("Js/Dialog/_FormValues"),
        new("Js/Dialog/_Intro"),
        new("Js/Dialog/_ManualResult"),
        new("Kbd/_ArrowKeys"),
        new("Kbd/_Basic"),
        new("Kbd/_InputGroup"),
        new("Kbd/_Intro"),
        new("Kbd/_KbdGroup"),
        new("Kbd/_ModifierKeys"),
        new("Kbd/_Tooltip"),
        new("Kbd/_WithIconAndText"),
        new("Kbd/_WithIcons"),
        new("LinkButton/_AdditionalAttributes"),
        new("LinkButton/_IconLeft"),
        new("LinkButton/_IconOnly"),
        new("LinkButton/_IconRight"),
        new("LinkButton/_Intro"),
        new("LinkButton/_SizesVariants"),
        new("LinkButton/_Url"),
        new("Pagination/_CustomContent"),
        new("Pagination/_Intro"),
        new("Pagination/_Url"),
        new("Popover/_ButtonGroup"),
        new("Popover/_Hover"),
        new("Popover/_Intro"),
        new("Popover/_JsApi"),
        new("Popover/_JsEvents"),
        new("Popover/_ManualDismiss"),
        new("Popover/_Offset"),
        new("Popover/_Position"),
        new("Progress/_FileUploadList"),
        new("Progress/_Intro"),
        new("Progress/_MinMax"),
        new("Progress/_WithLabel"),
        new("Radio/_Disabled"),
        new("Radio/_FieldExplicit"),
        new("Radio/_FieldImplicit"),
        new("Radio/_Intro"),
        new("Radio/_ManualValidationExplicit"),
        new("Radio/_ManualValidationImplicit"),
        new("Radio/_ModelBinding"),
        new("Radio/_Validation"),
        new("Select/_Disabled"),
        new("Select/_FieldExplicit"),
        new("Select/_FieldImplicit"),
        new("Select/_Groups"),
        new("Select/_Intro"),
        new("Select/_ManualValidationExplicit"),
        new("Select/_ManualValidationImplicit"),
        new("Select/_ModelBinding"),
        new("Select/_Sizes"),
        new("Select/_Validation"),
        new("Separator/_Horizontal"),
        new("Separator/_InList"),
        new("Separator/_Intro"),
        new("Separator/_Vertical"),
        new("Separator/_VerticalMenu"),
        new("Sheet/_Dismissing"),
        new("Sheet/_Intro"),
        new("Sheet/_JsApi"),
        new("Sheet/_JsEvents"),
        new("Sheet/_NoCloseButton"),
        new("Sheet/_ReturnFormValue"),
        new("Sheet/_Sides"),
        new("Sidebar/_AlternateTriggerIcon", "_CleanLayout"),
        new("Sidebar/_CollapsibleGroups", "_CleanLayout"),
        new("Sidebar/_CollapsibleIcon", "_CleanLayout"),
        new("Sidebar/_CollapsibleItems", "_CleanLayout"),
        new("Sidebar/_FloatingVariant", "_CleanLayout"),
        new("Sidebar/_InsetVariant", "_CleanLayout"),
        new("Sidebar/_Intro", "_CleanLayout"),
        new("Sidebar/_RightSide", "_CleanLayout"),
        new("Sidebar/_SidebarInDialog", "_CleanLayout"),
        new("Skeleton/_Avatar"),
        new("Skeleton/_Form"),
        new("Skeleton/_Intro"),
        new("Skeleton/_Table"),
        new("Skeleton/_Text"),
        new("Slider/_Disabled"),
        new("Slider/_Intro"),
        new("Slider/_ModelBinding"),
        new("Slider/_MultipleThumbs"),
        new("Slider/_Range"),
        new("Slider/_Steps"),
        new("Slider/_ThumbAlignment"),
        new("Slider/_Vertical"),
        new("Spinner/_Color"),
        new("Spinner/_InBadges"),
        new("Spinner/_InButtons"),
        new("Spinner/_InEmpty"),
        new("Spinner/_InInputGroup"),
        new("Spinner/_Intro"),
        new("Spinner/_Size"),
        new("Stack/_Align"),
        new("Stack/_Gap"),
        new("Stack/_Justify"),
        new("Switch/_Disabled"),
        new("Switch/_Intro"),
        new("Switch/_ModelBinding"),
        new("Switch/_Sizes"),
        new("Table/_Border"),
        new("Table/_Intro"),
        new("Table/_Select"),
        new("Tabs/_Active"),
        new("Tabs/_Disabled"),
        new("Tabs/_Icons"),
        new("Tabs/_IconsOnly"),
        new("Tabs/_Intro"),
        new("Tabs/_Line"),
        new("Tabs/_Orientation"),
        new("Tabs/_Url"),
        new("Textarea/_FieldExplicit"),
        new("Textarea/_FieldImplicit"),
        new("Textarea/_Intro"),
        new("Textarea/_ManualValidationExplicit"),
        new("Textarea/_ManualValidationImplicit"),
        new("Textarea/_ModelBinding"),
        new("Textarea/_Validation"),
        new("Toggle/_Disabled"),
        new("Toggle/_Intro"),
        new("Toggle/_ModelBinding"),
        new("Toggle/_Outline"),
        new("Toggle/_Sizes"),
        new("Toggle/_WithText"),
        new("ToggleGroup/_Intro"),
        new("ToggleGroup/_Joined"),
        new("ToggleGroup/_ModelBinding"),
        new("ToggleGroup/_Multiple"),
        new("ToggleGroup/_Outline"),
        new("ToggleGroup/_Sizes"),
        new("ToggleGroup/_Spacing"),
        new("ToggleGroup/_Vertical"),
        new("Tooltip/_Delay"),
        new("Tooltip/_Elements"),
        new("Tooltip/_Intro"),
        new("Tooltip/_JsApi"),
        new("Tooltip/_JsEvents"),
        new("Tooltip/_Offset"),
        new("Tooltip/_Position"),
    ];
}
