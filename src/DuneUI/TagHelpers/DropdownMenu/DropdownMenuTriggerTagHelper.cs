using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-dropdown-menu-trigger")]
public class DropdownMenuTriggerTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    [HtmlAttributeName("size")]
    public ButtonSize? Size { get; set; }

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
