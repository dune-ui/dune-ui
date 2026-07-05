using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     The content region of an accordion item, revealed when the item is expanded.
/// </summary>
[HtmlTargetElement("dui-accordion-item-content")]
public class AccordionItemContentTagHelper : DuneUITagHelperBase
{
    public AccordionItemContentTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "accordion-content");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ThemeToken("dui-accordion-content"),
                "overflow-hidden",
                "details-disabled-closed-content:hidden"
            )
        );

        var innerTagBuilder = new TagBuilder("div");
        innerTagBuilder.Attributes.Add(
            "class",
            BuildClassString(
                new ThemeToken("dui-accordion-content-inner"),
                "[&_a]:hover:text-foreground h-(--accordion-panel-height) data-ending-style:h-0 data-starting-style:h-0 [&_a]:underline [&_a]:underline-offset-3 [&_p:not(:last-child)]:mb-4",
                output.GetUserSuppliedClass()
            )
        );
        innerTagBuilder.InnerHtml.AppendHtml(await output.GetChildContentAsync());

        output.Content.AppendHtml(innerTagBuilder);
    }
}
