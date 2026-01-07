using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-sidebar-header")]
public class SidebarHeaderTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-header");
        output.Attributes.SetAttribute("data-sidebar", "header");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-sidebar-header"),
                "flex flex-col",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
