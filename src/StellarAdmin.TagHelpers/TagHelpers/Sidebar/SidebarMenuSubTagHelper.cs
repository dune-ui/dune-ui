using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-sidebar-menu-sub")]
public class SidebarMenuSubTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "ul";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-menu-sub");
        output.Attributes.SetAttribute("data-sidebar", "menu-sub");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "border-sidebar-border mx-3.5 flex min-w-0 translate-x-px flex-col gap-1 border-l px-2.5 py-0.5",
                "group-data-[collapsible=icon]:hidden",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
