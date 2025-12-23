using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-sidebar-group")]
public class SidebarGroupTagHelper : DuneUITagHelperBase
{
    public SidebarGroupTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-group");
        output.Attributes.SetAttribute("data-sidebar", "group");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                "relative flex w-full min-w-0 flex-col p-2",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
