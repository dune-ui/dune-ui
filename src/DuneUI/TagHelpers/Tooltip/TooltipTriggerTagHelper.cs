using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-tooltip-trigger")]
public class TooltipTriggerTagHelper : DuneUITagHelperBase
{
    public TooltipTriggerTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("x-bind", "trigger");
        output.Attributes.SetAttribute("x-ref", "trigger");

        output.Attributes.SetAttribute(
            "class",
            BuildClassString(null, ["w-fit inline-block", output.GetUserSuppliedClass()])
        );

        return Task.CompletedTask;
    }
}
