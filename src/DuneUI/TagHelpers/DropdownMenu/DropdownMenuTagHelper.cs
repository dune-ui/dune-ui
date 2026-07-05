using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     The root of a dropdown menu, pairing a trigger with its content and generating the shared id
///     that links them.
/// </summary>
[HtmlTargetElement("dui-dropdown-menu")]
public class DropdownMenuTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var menuId =
            output.Attributes.TryGetAttribute("id", out var idAttribute)
            && idAttribute.Value?.ToString() is { Length: > 0 } userId
                ? userId
                : $"--dui-dropdown-menu-{context.UniqueId}";

        SetContext(context, new DropdownMenuContext { MenuId = menuId });

        output.TagName = null;
        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
