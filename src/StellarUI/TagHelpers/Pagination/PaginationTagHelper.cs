using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-pagination")]
public class PaginationTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "nav";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("role", "navigation");
        output.Attributes.SetAttribute("aria-label", "pagination");
        output.Attributes.SetAttribute("data-slot", "pagination");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge("mx-auto flex w-full justify-center", output.GetUserSuppliedClass())
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
