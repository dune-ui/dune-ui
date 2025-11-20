using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sa-item-group")]
public class ItemGroupTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge("group/item-group flex flex-col", GetUserSpecifiedClass(output))
        );
        output.Attributes.SetAttribute("data-slot", "item-group");

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
