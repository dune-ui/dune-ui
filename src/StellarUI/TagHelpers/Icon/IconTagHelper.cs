using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-icon", Attributes = "name", TagStructure = TagStructure.WithoutEndTag)]
[OutputElementHint("svg")]
public class IconTagHelper : TagHelper
{
    public string? Name { get; set; }

    public static string[] GetAllIconNames()
    {
        return LucideIcons.IconDefinitions.Keys.ToArray();
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var iconRenderer = new IconRenderer();
        iconRenderer.Render(output, Name);
    }
}
