using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-dropdown-menu-radio-group")]
public class DropdownMenuRadioGroupTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    [HtmlAttributeName("value")]
    public string? Value { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var groupName = $"--dui-dropdown-menu-radio-{context.UniqueId}";
        SetContext(
            context,
            new DropdownMenuRadioGroupContext { GroupName = groupName, SelectedValue = Value }
        );

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("role", "group");
        output.Attributes.SetAttribute("data-slot", "dropdown-menu-radio-group");
        output.Attributes.SetAttribute("data-radio-group", groupName);

        var userClass = output.GetUserSuppliedClass();
        if (!string.IsNullOrEmpty(userClass))
        {
            output.Attributes.SetAttribute("class", userClass);
        }

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
