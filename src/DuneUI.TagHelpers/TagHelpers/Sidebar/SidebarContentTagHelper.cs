using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-sidebar-content")]
public class SidebarContentTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-content");
        output.Attributes.SetAttribute("data-sidebar", "content");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-sidebar-content"),
                "flex min-h-0 flex-1 flex-col overflow-auto group-data-[collapsible=icon]:overflow-hidden",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
