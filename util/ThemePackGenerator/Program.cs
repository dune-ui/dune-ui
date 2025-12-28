// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

namespace ThemePackGenerator;

public partial class Program
{
    public static async Task Main(string[] args)
    {
        var themePackFolder = @"C:\development\dune-ui\dune-ui\src\DuneUI.Core\Theming\ThemePacks";
        string[] themeFiles =
        [
            "https://raw.githubusercontent.com/shadcn-ui/ui/refs/heads/main/apps/v4/registry/styles/style-lyra.css",
            "https://raw.githubusercontent.com/shadcn-ui/ui/refs/heads/main/apps/v4/registry/styles/style-maia.css",
            "https://raw.githubusercontent.com/shadcn-ui/ui/refs/heads/main/apps/v4/registry/styles/style-mira.css",
            "https://raw.githubusercontent.com/shadcn-ui/ui/refs/heads/main/apps/v4/registry/styles/style-nova.css",
            "https://raw.githubusercontent.com/shadcn-ui/ui/refs/heads/main/apps/v4/registry/styles/style-vega.css",
        ];

        var httpClient = new HttpClient();

        foreach (var themeFile in themeFiles)
        {
            var responseMessage = await httpClient.GetAsync(themeFile);

            responseMessage.EnsureSuccessStatusCode();

            var responseText = await responseMessage.Content.ReadAsStringAsync();
            var themeStyles = ExtractComponentsFromThemeStyle(responseText)
                .AddFieldRadioGroupSupport()
                .RemoveAriaInvalidRing()
                .CreateInputValidationErrorClassesFromAriaInvalid()
                .ReplaceDuiCheckboxDataChecked()
                .ReplaceDuiRadioGroupItemDataChecked();

            var fileName = Path.GetFileNameWithoutExtension(themeFile)
                .Replace("style-", string.Empty);

            var themePackText = string.Join(
                Environment.NewLine,
                themeStyles.Select(pair =>
                    $"-{pair.Key}{Environment.NewLine}{pair.Value}{Environment.NewLine}"
                )
            );

            await File.WriteAllTextAsync(
                Path.Combine(
                    themePackFolder,
                    char.ToUpperInvariant(fileName[0]) + fileName[1..] + ".themepack"
                ),
                themePackText
            );
        }
    }

    [GeneratedRegex(@".cn-(?<name>\S+).*{\n(\s)*@apply(\s)+(?<value>.*);")]
    public static partial Regex StyleRegex();

    private static Dictionary<string, string> ExtractComponentsFromThemeStyle(string input)
    {
        var componentStyles = new Dictionary<string, string>();

        foreach (Match match in StyleRegex().Matches(input))
        {
            componentStyles.Add($"dui-{match.Groups["name"].Value}", match.Groups["value"].Value);
        }

        return componentStyles;
    }
}
