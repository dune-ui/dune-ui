using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis;

namespace DuneUI.Generators;

[Generator]
public class IconsFromJsonDefinitionGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var jsonFileProvider = context.AdditionalTextsProvider.Where(file =>
            Path.GetDirectoryName(file.Path) is { } directoryName
            && directoryName.EndsWith(Path.Combine("Icons", "Definitions"))
            && Path.GetExtension(file.Path) == ".json"
        );

        context.RegisterSourceOutput(
            jsonFileProvider,
            (sourceContext, provider) =>
            {
                var sourceText = provider.GetText();
                if (sourceText == null || sourceText.Lines.Count == 0)
                {
                    return;
                }

                var iconPackName = Path.GetFileNameWithoutExtension(provider.Path);
                try
                {
                    // Deserialize the JSON content
                    var data = JsonSerializer.Deserialize<Dictionary<string, List<SvgShape>>>(
                        sourceText.ToString()
                    )!;

                    var groupedByAlphabet = data.GroupBy(kvp => kvp.Key.Substring(0, 1));
                    foreach (var grouping in groupedByAlphabet)
                    {
                        var iconShapesSource = WriteIconShapes(
                            iconPackName,
                            grouping.Key.ToUpper(),
                            grouping.ToList()
                        );
                        sourceContext.AddSource(
                            $"{iconPackName}Icons.{grouping.Key}.g.cs",
                            iconShapesSource.ToString()
                        );
                    }

                    var iconDefinitionsSource = WriteIconDefinitions(iconPackName, data);
                    sourceContext.AddSource(
                        $"{iconPackName}Icons.g.cs",
                        iconDefinitionsSource.ToString()
                    );
                }
                catch (Exception ex)
                {
                    // Handle JSON parsing errors by reporting a diagnostic
                    sourceContext.ReportDiagnostic(
                        Diagnostic.Create(
                            new DiagnosticDescriptor(
                                "JSONGEN001",
                                "Invalid JSON format",
                                "The 'icon-nodes.json' file is not in a valid JSON format: {0}",
                                "JsonGenerator",
                                DiagnosticSeverity.Error,
                                true
                            ),
                            Location.None, // You can't link to a file, so just use Location.None
                            ex
                        )
                    );
                }
            }
        );
    }

    private string IconNameToPascalCase(string name)
    {
        var pascalCaseName = "Icon";
        var parts = name.Split('-');
        foreach (var part in parts)
        {
            if (!string.IsNullOrEmpty(part))
            {
                pascalCaseName += char.ToUpper(part[0]) + part.Substring(1);
            }
        }

        return pascalCaseName;
    }

    private StringBuilder WriteIconShapes(
        string iconPackName,
        string letter,
        List<KeyValuePair<string, List<SvgShape>>> icons
    )
    {
        var generatedSource = new StringBuilder();
        generatedSource.AppendLine("using System.Collections.Frozen;");
        generatedSource.AppendLine("using System.Collections.Immutable;");
        generatedSource.AppendLine();
        generatedSource.AppendLine("namespace DuneUI.Icons;");
        generatedSource.AppendLine();
        generatedSource.AppendLine($"internal static class {iconPackName}_{letter}_Icons");
        generatedSource.AppendLine("{");

        // Generate the static classes for the individual icons
        foreach (var iconKvp in icons)
        {
            generatedSource.AppendLine(
                $"    public static List<SvgShape> {IconNameToPascalCase(iconKvp.Key)} ="
            );
            generatedSource.AppendLine("    [");
            foreach (var iconShapeKvp in iconKvp.Value)
            {
                generatedSource.AppendLine("        new SvgShape(");
                generatedSource.AppendLine($"            \"{iconShapeKvp.Name}\",");
                generatedSource.AppendLine("            new Dictionary<string, string>");
                generatedSource.AppendLine("            {");
                foreach (var attribute in iconShapeKvp.Attributes)
                {
                    generatedSource.AppendLine(
                        $"                [\"{attribute.Key}\"] = \"{attribute.Value}\","
                    );
                }

                generatedSource.AppendLine("            }.ToImmutableDictionary()");
                generatedSource.AppendLine("        ),");
            }

            generatedSource.AppendLine("    ];");
            generatedSource.AppendLine();
        }

        generatedSource.AppendLine("}");

        return generatedSource;
    }

    private StringBuilder WriteIconDefinitions(
        string iconPackName,
        Dictionary<string, List<SvgShape>> icons
    )
    {
        var generatedSource = new StringBuilder();
        generatedSource.AppendLine("using System.Collections.Frozen;");
        generatedSource.AppendLine("using System.Collections.Immutable;");
        generatedSource.AppendLine();
        generatedSource.AppendLine("namespace DuneUI.Icons;");
        generatedSource.AppendLine();
        generatedSource.AppendLine($"internal static partial class {iconPackName}Icons");
        generatedSource.AppendLine("{");
        generatedSource.AppendLine(
            "    public static FrozenDictionary<string, IconDefinition> IconDefinitions = new Dictionary<"
        );
        generatedSource.AppendLine("        string,");
        generatedSource.AppendLine("        IconDefinition");
        generatedSource.AppendLine("    >");
        generatedSource.AppendLine("    {");
        foreach (var iconKvp in icons)
        {
            var declarationClassName =
                $"{iconPackName}_{iconKvp.Key.Substring(0, 1).ToUpper()}_Icons";

            generatedSource.AppendLine(
                $"        [\"{iconKvp.Key}\"] = new IconDefinition(SvgAttributes.ToImmutableDictionary(), {declarationClassName}.{IconNameToPascalCase(iconKvp.Key)}),"
            );
        }

        generatedSource.AppendLine("    }.ToFrozenDictionary();");
        generatedSource.AppendLine("}");

        return generatedSource;
    }

    [JsonConverter(typeof(SvgShapeJsonConverter))]
    private class SvgShape(string name, Dictionary<string, string> attributes)
    {
        public Dictionary<string, string> Attributes { get; } = attributes;

        public string Name { get; } = name;
    }

    private class SvgShapeJsonConverter : JsonConverter<SvgShape>
    {
        public override SvgShape Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException("Expected start of a JSON array.");
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException("Expected a string as the first element.");
            }

            var shapeName = reader.GetString();
            if (shapeName == null)
            {
                throw new JsonException("Expected a string as the first element.");
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected an object as the second element.");
            }

            // Deserialize the object directly into a PathData class
            var shapeAttributes = JsonSerializer.Deserialize<Dictionary<string, string>>(
                ref reader,
                options
            );
            if (shapeAttributes == null)
            {
                throw new JsonException("Expected a dictionary of shape attributes.");
            }

            // Move past the end of the array
            reader.Read();
            if (reader.TokenType != JsonTokenType.EndArray)
            {
                throw new JsonException("Expected end of a JSON array.");
            }

            return new SvgShape(shapeName, shapeAttributes);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SvgShape value,
            JsonSerializerOptions options
        )
        {
            throw new NotImplementedException();
        }
    }
}
