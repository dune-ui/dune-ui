using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-item-separator")]
public class ItemSeparatorTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    [HtmlAttributeName("is-decorative")]
    public bool IsDecorative { get; set; } = true;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge("my-0", GetUserSpecifiedClass(output))
        );
        output.Attributes.SetAttribute("data-slot", "item-separator");

        var separatorRenderer = new SeparatorRenderer(classMerger);
        separatorRenderer.Render(output, SeparatorOrientation.Horizontal, IsDecorative);
    }
}
