using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     Wraps a submenu, pairing a <c>dui-dropdown-menu-sub-trigger</c> with its
///     <c>dui-dropdown-menu-sub-content</c> and generating the shared id that links them.
/// </summary>
[HtmlTargetElement("dui-dropdown-menu-sub")]
public class DropdownMenuSubTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var subId =
            output.Attributes.TryGetAttribute("id", out var idAttribute)
            && idAttribute.Value?.ToString() is { Length: > 0 } userId
                ? userId
                : $"--dui-dropdown-menu-sub-{context.UniqueId}";

        SetContext(context, new DropdownMenuContext { MenuId = subId });

        output.TagName = null;
        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
