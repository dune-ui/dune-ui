using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-sidebar-menu-sub-item")]
public class SidebarMenuSubItemTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "li";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-menu-sub-item");
        output.Attributes.SetAttribute("data-sidebar", "menu-sub-item");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge("group/menu-sub-item relative", output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
