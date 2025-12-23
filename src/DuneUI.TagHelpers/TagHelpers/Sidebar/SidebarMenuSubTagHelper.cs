using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-sidebar-menu-sub")]
public class SidebarMenuSubTagHelper : DuneUITagHelperBase
{
    public SidebarMenuSubTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "ul";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-menu-sub");
        output.Attributes.SetAttribute("data-sidebar", "menu-sub");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                "border-sidebar-border mx-3.5 flex min-w-0 translate-x-px flex-col gap-1 border-l px-2.5 py-0.5",
                "group-data-[collapsible=icon]:hidden",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
