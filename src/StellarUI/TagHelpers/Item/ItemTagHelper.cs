using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-item")]
public class ItemTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    [HtmlAttributeName("size")]
    public ItemSize Size { get; set; } = ItemSize.Default;

    [HtmlAttributeName("variant")]
    public ItemVariant Variant { get; set; } = ItemVariant.Default;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        var itemRenderer = new ItemRenderer(classMerger);
        await itemRenderer.RenderAsync(context, output, Size, Variant);
    }
}
