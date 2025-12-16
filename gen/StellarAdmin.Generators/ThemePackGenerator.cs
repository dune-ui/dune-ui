using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;

namespace StellarUI.Generators;

[Generator]
public class ThemePackGenerator : IIncrementalGenerator
{
    private static readonly Regex StyleDeclarationRegex = new Regex(
        @"^-(?<name>dui-.*)",
        RegexOptions.Compiled
    );

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var themeCssFilesProvider = context.AdditionalTextsProvider.Where(file =>
            Path.GetFileName(file.Path).EndsWith(".themepack")
        );

        context.RegisterSourceOutput(
            themeCssFilesProvider,
            (sourceContext, provider) =>
            {
                var themeName = Path.GetFileNameWithoutExtension(provider.Path);
                var sourceText = provider.GetText();
                if (sourceText == null || sourceText.Lines.Count == 0)
                {
                    return;
                }

                string? currentStyle = null;
                var styles = new Dictionary<string, string>();
                foreach (var line in sourceText.Lines)
                {
                    if (line.ToString() is not { } lineText)
                    {
                        currentStyle = null;
                        continue;
                    }

                    if (StyleDeclarationRegex.Match(lineText) is { Success: true } declarationMatch)
                    {
                        currentStyle = declarationMatch.Groups["name"].Value.Trim();
                        continue;
                    }

                    if (currentStyle != null)
                    {
                        styles.Add(currentStyle, lineText);
                    }

                    currentStyle = null;
                }

                var generatedSource = new StringBuilder();
                generatedSource.AppendLine("namespace StellarAdmin.Theming;");
                generatedSource.AppendLine("");
                generatedSource.AppendLine($"public class {themeName}ThemePack : IThemePack");
                generatedSource.AppendLine("{");

                generatedSource.AppendLine(
                    "    public IDictionary<string, string> GetComponentClasses()"
                );
                generatedSource.AppendLine("    {");
                generatedSource.AppendLine("        return new Dictionary<string, string>");
                generatedSource.AppendLine("        {");
                foreach (var keyValuePair in styles)
                {
                    generatedSource.AppendLine(
                        $"            [\"{keyValuePair.Key}\"] = \"{keyValuePair.Value}\","
                    );
                }
                generatedSource.AppendLine("        };");
                generatedSource.AppendLine("    }");

                generatedSource.AppendLine("}");

                sourceContext.AddSource($"{themeName}ThemePack.g.cs", generatedSource.ToString());
            }
        );
    }
}
