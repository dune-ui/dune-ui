using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A selectable menu item (shadcn <c>DropdownMenuItem</c>). Renders as a
///     <c>&lt;div role="menuitem"&gt;</c>, or as an <c>&lt;a&gt;</c> when <c>href</c> is supplied.
/// </summary>
[HtmlTargetElement("dui-dropdown-menu-item")]
public class DropdownMenuItemTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    [HtmlAttributeName("disabled")]
    public bool? Disabled { get; set; }

    [HtmlAttributeName("href")]
    public string? Href { get; set; }

    [HtmlAttributeName("inset")]
    public bool? Inset { get; set; }

    [HtmlAttributeName("variant")]
    public DropdownMenuItemVariant? Variant { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveVariant = Variant ?? DropdownMenuItemVariant.Default;

        output.TagName = Href != null ? "a" : "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        if (Href != null)
        {
            output.Attributes.SetAttribute("href", Href);
        }

        output.Attributes.SetAttribute("role", "menuitem");
        output.Attributes.SetAttribute("tabindex", "-1");
        output.Attributes.SetAttribute("data-slot", "dropdown-menu-item");
        output.Attributes.SetAttribute("data-variant", effectiveVariant.GetDataAttributeText());
        if (Inset == true)
        {
            output.Attributes.SetAttribute("data-inset", "true");
        }

        if (Disabled == true)
        {
            output.Attributes.SetAttribute("data-disabled", "");
            output.Attributes.SetAttribute("aria-disabled", "true");
        }

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-dropdown-menu-item"),
                "group/dropdown-menu-item relative flex cursor-default items-center outline-hidden select-none data-disabled:pointer-events-none data-disabled:opacity-50 [&_svg]:pointer-events-none [&_svg]:shrink-0",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
