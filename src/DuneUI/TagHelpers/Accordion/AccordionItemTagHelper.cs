using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A single collapsible item within an accordion, rendered as a native
///     <c>&lt;details&gt;</c> element with a title and content region.
/// </summary>
[HtmlTargetElement("dui-accordion-item")]
public class AccordionItemTagHelper : DuneUITagHelperBase
{
    public AccordionItemTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "details";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "accordion-item");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ThemeToken("dui-accordion-item"),
                "group",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
