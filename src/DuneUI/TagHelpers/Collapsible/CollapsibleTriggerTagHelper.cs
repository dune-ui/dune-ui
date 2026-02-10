using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-collapsible-trigger")]
public class CollapsibleTriggerTagHelper : DuneUITagHelperBase
{
    public CollapsibleTriggerTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("x-bind", "trigger");

        output.Attributes.SetAttribute("data-slot", "collapsible-trigger");

        return Task.CompletedTask;
    }
}
