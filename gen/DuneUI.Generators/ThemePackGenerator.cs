using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;

namespace DuneUI.Generators;

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
                generatedSource.AppendLine("namespace DuneUI.Theming;");
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

    private KeyValuePair<string, string> Transform(KeyValuePair<string, string> input)
    {
        return input.Key switch
        {
            "dui-native-select" => AddInputValidationErrorVariantsFromAriaInvalid(
                input.Key,
                input.Value
            ),
            _ => input,
        };
    }

    private KeyValuePair<string, string> AddInputValidationErrorVariantsFromAriaInvalid(
        string key,
        string value
    )
    {
        var sb = new StringBuilder();

        var individualClasses = value.Split([' '], StringSplitOptions.RemoveEmptyEntries);

        foreach (var individualClass in individualClasses)
        {
            sb.Append(individualClass);
            sb.Append(" ");

            if (individualClass.Contains("aria-invalid:"))
            {
                sb.Append(individualClass.Replace("aria-invalid:", "[&.input-validation-error]:"));
                sb.Append(" ");
            }
        }

        return new KeyValuePair<string, string>(key, sb.ToString());
    }
}
