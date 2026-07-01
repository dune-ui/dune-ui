using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-dropdown-menu-content")]
public class DropdownMenuContentTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    [HtmlAttributeName("position")]
    public PositionArea? Position { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
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
                new ThemeToken("dui-menu-translucent"),
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
