using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-accordion-item-content")]
public class AccordionItemContentTagHelper : StellarTagHelper
{
    public AccordionItemContentTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "accordion-content");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ComponentName("dui-accordion-content"),
                "overflow-hidden",
                "details-disabled-closed-content:hidden",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
