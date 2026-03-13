using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-popover-header")]
public class PopoverHeaderTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "popover-header");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(new ThemeToken("dui-popover-header"), output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
