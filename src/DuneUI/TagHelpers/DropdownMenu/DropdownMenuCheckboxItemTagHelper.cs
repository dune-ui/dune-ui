using DuneUI.Icons;
using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A menu item with a checkable state, rendered as a
///     <c>&lt;div role="menuitemcheckbox"&gt;</c> with a check indicator.
/// </summary>
[HtmlTargetElement("dui-dropdown-menu-checkbox-item")]
public class DropdownMenuCheckboxItemTagHelper : DuneUITagHelperBase
{
    private readonly IIconManager _iconManager;

    /// <summary>
    ///     Whether the item is checked.
    /// </summary>
    /// <remarks>
    ///     Defaults to <c>false</c>.
    /// </remarks>
    [HtmlAttributeName("checked")]
    public bool? Checked { get; set; }

    /// <summary>
    ///     Whether clicking the item closes the menu. Checkbox items stay open on click unless this
    ///     is set.
    /// </summary>
    [HtmlAttributeName("close-on-click")]
    public bool? CloseOnClick { get; set; }

    /// <summary>
    ///     Whether the item is disabled.
    /// </summary>
    [HtmlAttributeName("disabled")]
    public bool? Disabled { get; set; }

    /// <summary>
    ///     Whether the item is inset, aligning its text with items that have a leading icon.
    /// </summary>
    [HtmlAttributeName("inset")]
    public bool? Inset { get; set; }

    public DropdownMenuCheckboxItemTagHelper(
        ThemeManager themeManager,
        ICssClassMerger classMerger,
        IIconManager iconManager
    )
        : base(themeManager, classMerger)
    {
        _iconManager = iconManager ?? throw new ArgumentNullException(nameof(iconManager));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var isChecked = Checked ?? false;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("role", "menuitemcheckbox");
        output.Attributes.SetAttribute("tabindex", "-1");
        output.Attributes.SetAttribute("aria-checked", isChecked ? "true" : "false");
        output.Attributes.SetAttribute("data-slot", "dropdown-menu-checkbox-item");
        output.Attributes.SetAttribute("data-state", isChecked ? "checked" : "unchecked");
        if (Inset == true)
        {
            output.Attributes.SetAttribute("data-inset", "true");
        }

        // Only emit when the author overrides the default (checkbox items stay open on click).
        if (CloseOnClick.HasValue)
        {
            output.Attributes.SetAttribute(
                "data-close-on-click",
                CloseOnClick.Value ? "true" : "false"
            );
        }

        if (Disabled == true)
        {
            output.Attributes.SetAttribute("data-disabled", "");
            output.Attributes.SetAttribute("aria-disabled", "true");
        }

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-dropdown-menu-checkbox-item"),
                "relative flex cursor-default items-center outline-hidden select-none data-disabled:pointer-events-none data-disabled:opacity-50 [&_svg]:pointer-events-none [&_svg]:shrink-0",
                output.GetUserSuppliedClass()
            )
        );

        var indicator = new TagBuilder("span");
        indicator.Attributes["data-slot"] = "dropdown-menu-checkbox-item-indicator";
        indicator.Attributes["class"] = ClassMerger.Merge(
            new ThemeToken("dui-dropdown-menu-item-indicator"),
            "pointer-events-none",
            isChecked ? string.Empty : "hidden"
        );
        indicator.InnerHtml.AppendHtml(
            DropdownMenuInternals.RenderIcon(
                context,
                ThemeManager,
                ClassMerger,
                _iconManager,
                "check",
                "size-4"
            )
        );

        var childContent = await output.GetChildContentAsync();
        output.Content.SetHtmlContent(indicator);
        output.Content.AppendHtml(childContent);
    }
}
