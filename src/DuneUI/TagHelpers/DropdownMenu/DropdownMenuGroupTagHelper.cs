using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-dropdown-menu-group")]
public class DropdownMenuGroupTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("role", "group");
        output.Attributes.SetAttribute("data-slot", "dropdown-menu-group");

        var userClass = output.GetUserSuppliedClass();
        if (!string.IsNullOrEmpty(userClass))
        {
            output.Attributes.SetAttribute("class", userClass);
        }

        return Task.CompletedTask;
    }
}
