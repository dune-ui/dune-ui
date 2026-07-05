using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     Displays a keyboard shortcut hint, aligned to the trailing edge of a menu item.
/// </summary>
[HtmlTargetElement("dui-dropdown-menu-shortcut")]
public class DropdownMenuShortcutTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "dropdown-menu-shortcut");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-dropdown-menu-shortcut"),
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
