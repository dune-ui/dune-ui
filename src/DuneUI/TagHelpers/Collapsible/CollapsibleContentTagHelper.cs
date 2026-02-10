using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-collapsible-content")]
public class CollapsibleContentTagHelper : DuneUITagHelperBase
{
    public CollapsibleContentTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("x-bind", "content");

        output.Attributes.SetAttribute("data-slot", "collapsible-content");

        return Task.CompletedTask;
    }
}
