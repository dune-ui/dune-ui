using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-sidebar-menu-sub-item")]
public class SidebarMenuSubItemTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "li";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-menu-sub-item");
        output.Attributes.SetAttribute("data-sidebar", "menu-sub-item");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge("group/menu-sub-item relative", output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
