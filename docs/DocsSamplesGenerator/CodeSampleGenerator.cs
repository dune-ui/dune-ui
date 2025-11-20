using System.Text.RegularExpressions;
using DocsSamples;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DocsSamplesGenerator;

public class CodeSampleGenerator(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private static readonly string PageSourceCodeFolder =
        @"C:\development\stellar-admin\stellar-admin\docs\DocsSamples\Pages";

    private static readonly string DocsProjectRootFolder = @"C:\development\stellar-admin\website";

    private static readonly string RenderedPagesOutputFolder =
        DocsProjectRootFolder + @"\public\demo\tag-helpers";

    private static readonly string PagesSourceCodeOutputFolder =
        DocsProjectRootFolder + @"\content\docs\tag-helpers\components\_include";

    private static readonly string DownloadedAssetsOutputFolder =
        DocsProjectRootFolder + @"\public\demo\tag-helpers\assets";

    [Theory]
    [InlineData("/css/site.css")]
    [InlineData("/_content/StellarAdmin.TagHelpers/stellar-admin.css")]
    public async Task DownloadDemoPageAssets(string url)
    {
        var client = factory.CreateClient();

        // Get the rendered page content
        var response = await client.GetAsync(url);

        // Save file
        if (!Directory.Exists(DownloadedAssetsOutputFolder))
        {
            Directory.CreateDirectory(DownloadedAssetsOutputFolder);
        }

        await File.WriteAllTextAsync(
            Path.Combine(DownloadedAssetsOutputFolder, Path.GetFileName(url)),
            await response.Content.ReadAsStringAsync()
        );
    }

    [Theory]
    [MemberData(nameof(ListDemoPages))]
    public async Task GenerateDemoPageSourceFiles(string page)
    {
        var sourceFile = PageSourceCodeFolder + page.Replace("/", @"\") + ".cshtml";

        var readSourceLines = await File.ReadAllLinesAsync(sourceFile);

        var cleanedLines = new List<string>();
        var processedDirectives = false;
        var charactersToDelete = 0;
        foreach (var sourceLine in readSourceLines)
        {
            // Read past the directives
            if (
                !processedDirectives
                && (sourceLine.StartsWith("@") || string.IsNullOrEmpty(sourceLine))
            )
            {
                continue;
            }

            processedDirectives = true;

            if (sourceLine.IndexOf("<!-- code end -->", StringComparison.Ordinal) >= 0)
            {
                break;
            }

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
            cleanedLines.Add(x);
        }

        cleanedLines.Insert(0, "```razor");
        cleanedLines.Add("```");

        var filename = PagesSourceCodeOutputFolder + @"\" + GenerateFilename(page) + ".mdx";
        await File.WriteAllLinesAsync(filename, cleanedLines);
    }

    public static TheoryData<string> ListDemoPages()
    {
        return
        [
            "/Alert/Actions",
            "/Alert/CustomCss",
            "/Alert/Icon",
            "/Alert/Intro",
            "/Alert/Variants",
            "/Badge/CustomCss",
            "/Badge/Intro",
            "/Badge/Variants",
            "/Breadcrumb/Collapsed",
            "/Breadcrumb/CustomCss",
            "/Breadcrumb/CustomSeparator",
            "/Breadcrumb/Icons",
            "/Breadcrumb/Intro",
            "/Button/AdditionalAttributes",
            "/Button/Icon",
            "/Button/Intro",
            "/Button/Sizes",
            "/Button/Spinner",
            "/Button/Variants",
            "/ButtonGroup/Intro",
            "/ButtonGroup/Nested",
            "/ButtonGroup/Orientation",
            "/ButtonGroup/Separator",
            "/ButtonGroup/Sizes",
            "/ButtonGroup/Text",
            "/Card/Image",
            "/Card/Intro",
            "/Card/SimpleText",
            "/Card/StatsCard",
            "/Empty/Intro",
            "/Empty/Avatar",
            "/Empty/CustomCss",
            "/Field/Intro",
            "/Field/Implicit",
            "/Field/Input",
            "/Field/InputImplicit",
            "/Field/Textarea",
            "/Field/TextareaImplicit",
            "/Field/Select",
            "/Field/SelectImplicit",
            "/Field/Checkbox",
            "/Field/CheckboxImplicit",
            "/Field/Radio",
            "/Field/RadioImplicit",
            "/Field/Fieldset",
            "/Field/FieldsetImplicit",
            "/Field/FieldGroup",
            "/Field/FieldGroupImplicit",
            "/Group/Align",
            "/Group/Gap",
            "/Group/Justify",
            "/Icon/Color",
            "/Icon/Intro",
            "/Icon/Size",
            "/Icon/StrokeWidth",
            "/Input/DataBinding",
            "/Input/ImplicitField",
            "/Input/InputTypeDataBinding",
            "/Input/InputTypes",
            "/Input/Intro",
            "/Input/WithField",
            "/Item/Avatar",
            "/Item/Group",
            "/Item/Header",
            "/Item/Icon",
            "/Item/Image",
            "/Item/Intro",
            "/Item/Link",
            "/Item/Sizes",
            "/Item/Variants",
            "/Kbd/Button",
            "/Kbd/Group",
            "/Kbd/Intro",
            "/LinkButton/AdditionalAttributes",
            "/LinkButton/Icon",
            "/LinkButton/Intro",
            "/LinkButton/Sizes",
            "/LinkButton/Url",
            "/LinkButton/Variants",
            "/Pagination/CustomContent",
            "/Pagination/Intro",
            "/Pagination/Url",
            "/Progress/CustomCss",
            "/Progress/Intro",
            "/Progress/MinMax",
            "/Separator/Intro",
            "/Separator/Orientation",
            "/Skeleton/Intro",
            "/Skeleton/Card",
            "/Spinner/Color",
            "/Spinner/Intro",
            "/Spinner/Size",
            "/Stack/Align",
            "/Stack/Gap",
            "/Stack/Justify",
            "/Table/Border",
            "/Table/Intro",
        ];
    }

    [Theory]
    [MemberData(nameof(ListDemoPages))]
    public async Task RenderDemoPageOutput(string page)
    {
        var client = factory.CreateClient();

        // Get the rendered page content
        var response = await client.GetAsync($"{page}?clean");

        // Fix up the content
        var content = FixDemoContent(await response.Content.ReadAsStringAsync());

        // Write the content to the file
        if (!Directory.Exists(RenderedPagesOutputFolder))
        {
            Directory.CreateDirectory(RenderedPagesOutputFolder);
        }

        await File.WriteAllTextAsync(
            Path.Combine(RenderedPagesOutputFolder, $"{GenerateFilename(page)}.html"),
            content
        );
    }

    private static string FixDemoContent(string input)
    {
        // Fix the stylesheet references
        return input
            .Replace("href=\"/css/site.css\"", "href=\"/demo/tag-helpers/assets/site.css\"")
            .Replace(
                "href=\"/_content/StellarAdmin.TagHelpers/stellar-admin.css\"",
                "href=\"/demo/tag-helpers/assets/stellar-admin.css\""
            );
    }

    private static string GenerateFilename(string input)
    {
        // Simple kekab-case converter
        return Regex
            .Replace(input.Replace("/", ""), "(?!^)([A-Z])", "-$1", RegexOptions.Compiled)
            .Trim()
            .ToLower();
    }
}
