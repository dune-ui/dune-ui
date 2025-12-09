using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-accordion")]
public class AccordionTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.SetAttribute("data-slot", "accordion");

        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge("w-full", output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
