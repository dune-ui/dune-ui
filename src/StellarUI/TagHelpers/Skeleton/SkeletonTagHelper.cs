using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-skeleton")]
public class SkeletonTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "skeleton");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge("bg-accent animate-pulse rounded-md", output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
