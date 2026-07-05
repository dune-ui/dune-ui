using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A button rendered as an entry within a nested sidebar submenu.
/// </summary>
[HtmlTargetElement("dui-sidebar-menu-sub-button")]
public class SidebarMenuSubButtonTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    /// <summary>
    ///     The size of the submenu button.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="SidebarMenuSubLinkSize.Medium" />.
    /// </remarks>
    public SidebarMenuSubLinkSize? Size { get; set; }

    /// <summary>
    ///     Whether this button represents the active item; when <c>true</c> the button is
    ///     marked with a <c>data-active</c> attribute for styling.
    /// </summary>
    /// <remarks>
    ///     Defaults to <c>false</c>.
    /// </remarks>
    public bool? IsActive { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveSize = Size ?? SidebarMenuSubLinkSize.Medium;

        output.TagName = "button";
        output.TagMode = TagMode.StartTagAndEndTag;

        if (!output.Attributes.ContainsName("type"))
        {
            output.Attributes.SetAttribute("type", "button");
        }

        output.Attributes.SetAttribute("data-slot", "sidebar-menu-sub-button");
        output.Attributes.SetAttribute("data-sidebar", "menu-sub-button");
        output.Attributes.SetAttribute("data-size", effectiveSize.GetDataAttributeText());
        if (IsActive ?? false)
        {
            output.Attributes.SetAttribute("data-active", true);
        }

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-sidebar-menu-sub-button"),
                "flex w-full min-w-0 -translate-x-px items-center overflow-hidden outline-hidden group-data-[collapsible=icon]:hidden disabled:pointer-events-none disabled:opacity-50 aria-disabled:pointer-events-none aria-disabled:opacity-50 [&>span:last-child]:truncate [&>svg]:shrink-0",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
