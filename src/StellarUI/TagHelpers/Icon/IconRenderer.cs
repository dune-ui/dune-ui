using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

internal class IconRenderer
{
    private static readonly Dictionary<string, string> DefaultValues = new()
    {
        ["xmlns"] = "http://www.w3.org/2000/svg",
        ["width"] = "24",
        ["height"] = "24",
        ["viewBox"] = "0 0 24 24",
        ["fill"] = "none",
        ["stroke"] = "currentColor",
        ["stroke-width"] = "2",
        ["stroke-linecap"] = "round",
        ["stroke-linejoin"] = "round",
    };

    public void Render(TagHelperOutput output, string? iconName)
    {
        if (
            !LucideIcons.IconDefinitions.TryGetValue(
                iconName ?? "circle-alert",
                out var iconDefinition
            )
        )
            iconDefinition = LucideIcons.IconDefinitions["circle-alert"];

        output.TagName = "svg";
        output.TagMode = TagMode.StartTagAndEndTag;

        foreach (var (name, value) in DefaultValues)
            if (!output.Attributes.ContainsName(name))
                output.Attributes.SetAttribute(name, value);

        foreach (var shape in iconDefinition)
        {
            var shapeBuilder = new TagBuilder(shape.Name);
            foreach (var (attributeName, attributeValue) in shape.Attributes)
                shapeBuilder.Attributes.Add(attributeName, attributeValue);

            output.Content.AppendHtml(shapeBuilder);
        }
    }
}