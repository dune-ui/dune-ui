using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-accordion-item")]
public class AccordionItemTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "details";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "accordion-item");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge("group border-b last:border-b-0", output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
