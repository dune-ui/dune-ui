using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-sidebar-menu")]
public class SidebarMenuTagHelper : DuneUITagHelperBase
{
    public SidebarMenuTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "ul";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-menu");
        output.Attributes.SetAttribute("data-sidebar", "menu");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge("flex w-full min-w-0 flex-col gap-1", output.GetUserSuppliedClass())
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
