using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis;

namespace StellarUI.Generators
{
    [Generator]
    public class LucideIconsGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var jsonFileProvider = context.AdditionalTextsProvider.Where(file =>
                Path.GetFileName(file.Path) == "icon-nodes.json"
            );

            var jsonDataProvider = jsonFileProvider.Select(
                (file, cancellationToken) => file.GetText(cancellationToken)?.ToString()
            );

            context.RegisterSourceOutput(
                jsonDataProvider,
                (sourceContext, jsonContent) =>
                {
                    if (string.IsNullOrEmpty(jsonContent))
                    {
                        return;
                    }

                    try
                    {
                        // Deserialize the JSON content
                        var data = JsonSerializer.Deserialize<Dictionary<string, List<SvgShape>>>(
                            jsonContent!
                        )!;

                        // Build the C# source file based on the data
                        var generatedSource = new StringBuilder();
                        generatedSource.AppendLine("using System.Collections.Frozen;");
                        generatedSource.AppendLine("using System.Collections.Immutable;");
                        generatedSource.AppendLine();
                        generatedSource.AppendLine("namespace StellarUI;");
                        generatedSource.AppendLine();
                        generatedSource.AppendLine("internal static partial class LucideIcons");
                        generatedSource.AppendLine("{");

                        // Generate the static classes for the individual icons
                        foreach (var iconKvp in data)
                        {
                            generatedSource.AppendLine(
                                $"    public static List<SvgShape> {IconNameToPascalCase(iconKvp.Key)} ="
                            );
                            generatedSource.AppendLine("    [");
                            foreach (var iconShapeKvp in iconKvp.Value)
                            {
                                generatedSource.AppendLine("        new SvgShape(");
                                generatedSource.AppendLine($"            \"{iconShapeKvp.Name}\",");
                                generatedSource.AppendLine(
                                    "            new Dictionary<string, string>"
                                );
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

                        // Generate the static dictionary for icon definition lookup
                        generatedSource.AppendLine(
                            "    public static FrozenDictionary<string, List<SvgShape>> IconDefinitions = new Dictionary<"
                        );
                        generatedSource.AppendLine("        string,");
                        generatedSource.AppendLine("        List<SvgShape>");
                        generatedSource.AppendLine("    >");
                        generatedSource.AppendLine("    {");
                        foreach (var iconKvp in data)
                        {
                            generatedSource.AppendLine(
                                $"        [\"{iconKvp.Key}\"] = {IconNameToPascalCase(iconKvp.Key)},"
                            );
                        }

                        generatedSource.AppendLine("    }.ToFrozenDictionary();");
                        generatedSource.AppendLine("}");

                        // Add the generated source to the compilation
                        sourceContext.AddSource("LucideIcons.g.cs", generatedSource.ToString());
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
            string pascalCaseName = "Icon";
            string[] parts = name.Split('-');
            foreach (string part in parts)
            {
                if (!string.IsNullOrEmpty(part))
                {
                    pascalCaseName += char.ToUpper(part[0]) + part.Substring(1);
                }
            }

            return pascalCaseName;
        }

        [JsonConverter(typeof(SvgShapeJsonConverter))]
        private class SvgShape(string name, Dictionary<string, string> attributes)
        {
            public string Name { get; } = name;

            public Dictionary<string, string> Attributes { get; } = attributes;
        };

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
}
