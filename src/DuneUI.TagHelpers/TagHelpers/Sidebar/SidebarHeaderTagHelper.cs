using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-sidebar-header")]
public class SidebarHeaderTagHelper : DuneUITagHelperBase
{
    public SidebarHeaderTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-header");
        output.Attributes.SetAttribute("data-sidebar", "header");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge("flex flex-col gap-2 p-2", output.GetUserSuppliedClass())
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
