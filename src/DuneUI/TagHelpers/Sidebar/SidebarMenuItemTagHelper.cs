using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A single item within a sidebar menu, rendered as a list item.
/// </summary>
[HtmlTargetElement("dui-sidebar-menu-item")]
public class SidebarMenuItemTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "li";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-menu-item");
        output.Attributes.SetAttribute("data-sidebar", "menu-item");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge("group/menu-item relative", output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
