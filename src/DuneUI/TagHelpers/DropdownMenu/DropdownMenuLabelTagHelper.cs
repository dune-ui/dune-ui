using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-dropdown-menu-label")]
public class DropdownMenuLabelTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    [HtmlAttributeName("inset")]
    public bool? Inset { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "dropdown-menu-label");
        if (Inset == true)
        {
            output.Attributes.SetAttribute("data-inset", "true");
        }

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-dropdown-menu-label"),
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
