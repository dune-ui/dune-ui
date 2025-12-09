using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-accordion-item-content")]
public class AccordionItemContentTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "accordion-content");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "overflow-hidden text-sm pt-0 pb-4",
                "details-disabled-closed-content:hidden",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
