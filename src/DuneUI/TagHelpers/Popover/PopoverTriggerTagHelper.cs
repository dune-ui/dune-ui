using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-popover-trigger")]
public class PopoverTriggerTagHelper : DuneUITagHelperBase
{
    public PopoverTriggerTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("x-bind", "trigger");
        output.Attributes.SetAttribute("x-ref", "trigger");

        output.Attributes.SetAttribute("data-slot", "popover-trigger");

        return Task.CompletedTask;
    }
}
