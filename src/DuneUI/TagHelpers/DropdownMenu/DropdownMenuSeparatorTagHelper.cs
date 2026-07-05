using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A horizontal divider that visually separates groups of menu items.
/// </summary>
[HtmlTargetElement("dui-dropdown-menu-separator")]
public class DropdownMenuSeparatorTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("role", "separator");
        output.Attributes.SetAttribute("aria-orientation", "horizontal");
        output.Attributes.SetAttribute("data-slot", "dropdown-menu-separator");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-dropdown-menu-separator"),
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
