using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A vertically stacked set of collapsible items, each of which can be expanded to reveal its content.
/// </summary>
[HtmlTargetElement("dui-accordion")]
public class AccordionTagHelper : DuneUITagHelperBase
{
    public AccordionTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.SetAttribute("data-slot", "accordion");

        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ThemeToken("dui-accordion"),
                "flex w-full flex-col",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
