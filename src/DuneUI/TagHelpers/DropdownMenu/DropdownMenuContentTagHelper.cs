using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-dropdown-menu-content")]
public class DropdownMenuContentTagHelper(
    ThemeManager themeManager,
    ICssClassMerger classMerger,
    IOptions<DuneUIOptions> options
) : DuneUITagHelperBase(themeManager, classMerger)
{
    [HtmlAttributeName("position")]
    public PositionArea? Position { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var menuOptions = options.Value.Menu;
        var effectivePosition = Position ?? PositionArea.BottomSpanRight;

        output.TagName = "del-dropdown-menu";
        output.TagMode = TagMode.StartTagAndEndTag;

        var menuId = GetContext<DropdownMenuContext>(context)?.MenuId;
        if (menuId != null && !output.Attributes.ContainsName("id"))
        {
            output.Attributes.SetAttribute("id", menuId);
        }

        if (!output.Attributes.ContainsName("popover"))
        {
            output.Attributes.SetAttribute("popover", "");
        }

        output.Attributes.SetAttribute("role", "menu");
        output.Attributes.SetAttribute("data-slot", "dropdown-menu-content");
        output.Attributes.SetAttribute(
            "data-side",
            DropdownMenuInternals.GetSideDataAttribute(effectivePosition)
        );
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-dropdown-menu-content"),
                new ThemeToken("dui-dropdown-menu-content-logical"),
                MenuSurfaceInternals.ColorToken(menuOptions.Color),
                MenuSurfaceInternals.AppearanceToken(menuOptions.Appearance),
                MenuSurfaceInternals.AccentToken(menuOptions.Accent),
                DropdownMenuInternals.ContentStaticClasses,
                effectivePosition.GetTailwindClassName(),
                DropdownMenuInternals.GetMarginClassName(effectivePosition),
                DropdownMenuInternals.ContentTransitionClasses,
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
