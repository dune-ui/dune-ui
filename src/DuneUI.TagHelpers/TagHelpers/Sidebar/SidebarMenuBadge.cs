using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-sidebar-menu-badge")]
public class SidebarMenuBadge(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-menu-badge");
        output.Attributes.SetAttribute("data-sidebar", "menu-badge");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-sidebar-menu-badge"),
                "flex items-center justify-center tabular-nums select-none group-data-[collapsible=icon]:hidden",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}