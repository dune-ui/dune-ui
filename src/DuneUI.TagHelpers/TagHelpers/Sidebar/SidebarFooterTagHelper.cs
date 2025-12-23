using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-sidebar-footer")]
public class SidebarFooterTagHelper : DuneUITagHelperBase
{
    public SidebarFooterTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-footer");
        output.Attributes.SetAttribute("data-sidebar", "footer");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge("flex flex-col gap-2 p-2", output.GetUserSuppliedClass())
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
