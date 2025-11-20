using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Icons;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-icon", TagStructure = TagStructure.WithoutEndTag)]
[OutputElementHint("svg")]
public class IconTagHelper : TagHelper
{
    private readonly IIconManager _iconManager;
    private static readonly List<SvgShape> NotFoundIcon =
    [
        new SvgShape(
            "path",
            new Dictionary<string, string>
            {
                ["d"] =
                    "M6 22a2 2 0 0 1-2-2V4a2 2 0 0 1 2-2h8a2.4 2.4 0 0 1 1.704.706l3.588 3.588A2.4 2.4 0 0 1 20 8v12a2 2 0 0 1-2 2z",
            }.ToImmutableDictionary()
        ),
        new SvgShape(
            "path",
            new Dictionary<string, string> { ["d"] = "M12 9v4" }.ToImmutableDictionary()
        ),
        new SvgShape(
            "path",
            new Dictionary<string, string> { ["d"] = "M12 17h.01" }.ToImmutableDictionary()
        ),
    ];

    public IconTagHelper(IIconManager iconManager)
    {
        _iconManager = iconManager ?? throw new ArgumentNullException(nameof(iconManager));
    }

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

    [HtmlAttributeName("name")]
    public string? Name { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var iconDefinition =
            Name != null && _iconManager.TryGetIcon(Name, out var foundIcon) && foundIcon != null
                ? foundIcon
                : NotFoundIcon;

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
