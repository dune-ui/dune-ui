using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-empty-content")]
public class EmptyContentTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "empty-content");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "flex w-full max-w-sm min-w-0 flex-col items-center gap-4 text-sm text-balance",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.SetHtmlContent(await output.GetChildContentAsync());
    }
}
