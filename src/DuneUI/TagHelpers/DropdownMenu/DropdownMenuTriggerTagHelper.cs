using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     The button that toggles a dropdown menu and anchors its content, styled with button variant
///     and size options.
/// </summary>
[HtmlTargetElement("dui-dropdown-menu-trigger")]
public class DropdownMenuTriggerTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    /// <summary>
    ///     The size of the trigger button.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="ButtonSize.Default" />.
    /// </remarks>
    [HtmlAttributeName("size")]
    public ButtonSize? Size { get; set; }

    /// <summary>
    ///     The visual style of the trigger button.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="ButtonVariant.Outline" />.
    /// </remarks>
    [HtmlAttributeName("variant")]
    public ButtonVariant? Variant { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("type", "button");
        output.Attributes.SetAttribute("data-slot", "dropdown-menu-trigger");
        output.Attributes.SetAttribute("aria-haspopup", "menu");

        // Native popover invoker: clicking toggles the menu and establishes the implicit
        // CSS anchor reference the content positions against.
        var menuId = GetContext<DropdownMenuContext>(context)?.MenuId;
        if (menuId != null)
        {
            output.Attributes.SetAttribute("popovertarget", menuId);
        }

        ButtonRenderingHelper.RenderAttributes(
            output,
            ClassMerger,
            Variant ?? ButtonVariant.Outline,
            Size ?? ButtonSize.Default
        );

        return Task.CompletedTask;
    }
}
