using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-item-actions")]
public class ItemActionsTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge("flex items-center gap-2", GetUserSpecifiedClass(output))
        );
        output.Attributes.SetAttribute("data-slot", "item-actions");

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
