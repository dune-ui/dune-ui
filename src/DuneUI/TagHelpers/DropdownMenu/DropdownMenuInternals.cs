using DuneUI.Icons;
using DuneUI.Theming;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     Shared helpers for the Dropdown Menu family: the popover-content positioning
///     classes (mirroring <see cref="PopoverTagHelper" />) and indicator/chevron icon
///     rendering.
/// </summary>
internal static class DropdownMenuInternals
{
    /// <summary>
    ///     Static (cross-theme) utility classes for a menu content popup. Anchor-positioning
    ///     vars (<c>max-h-(--available-height)</c> etc.) are dropped — DuneUI uses the native
    ///     <c>popover</c> + CSS anchor positioning instead (justified divergence, same as
    ///     <see cref="PopoverTagHelper" />).
    /// </summary>
    public const string ContentStaticClasses =
        "z-50 overflow-x-hidden overflow-y-auto outline-hidden try-flip-all";

    /// <summary>
    ///     Native-popover open/close transition. Keyed off the <c>:popover-open</c> state via
    ///     the <c>open:</c>/<c>not-open:</c> variants, matching <see cref="PopoverTagHelper" />.
    ///     The token's <c>data-open:animate-in</c> slide animations stay inert (we don't set
    ///     <c>data-open</c>); the directional slide is the justified divergence.
    /// </summary>
    public const string ContentTransitionClasses =
        "duration-200 ease-in opacity-100 not-open:opacity-0 starting:open:opacity-0 [transition-property:opacity,display,overlay] [transition-behavior:allow-discrete]";

    /// <summary>The gap between the trigger and the panel, on the appropriate side.</summary>
    public static string GetMarginClassName(PositionArea area) =>
        area switch
        {
            PositionArea.TopCenter
            or PositionArea.TopSpanLeft
            or PositionArea.TopSpanRight
            or PositionArea.TopLeft
            or PositionArea.TopRight
            or PositionArea.Top => "mb-2",
            PositionArea.LeftCenter
            or PositionArea.LeftSpanTop
            or PositionArea.LeftSpanBottom
            or PositionArea.Left => "me-2",
            PositionArea.BottomCenter
            or PositionArea.BottomSpanLeft
            or PositionArea.BottomSpanRight
            or PositionArea.BottomLeft
            or PositionArea.BottomRight
            or PositionArea.Bottom => "mt-2",
            PositionArea.RightCenter
            or PositionArea.RightSpanTop
            or PositionArea.RightSpanBottom
            or PositionArea.Right => "ms-2",
            _ => string.Empty,
        };

    /// <summary>
    ///     Best-effort <c>data-side</c> from the requested placement. On viewport-collision
    ///     flip (<c>try-flip-all</c>) the rendered side can differ — acceptable, since the
    ///     directional slide animation is not used (see <see cref="ContentTransitionClasses" />).
    /// </summary>
    public static string GetSideDataAttribute(PositionArea area) =>
        area switch
        {
            PositionArea.TopCenter
            or PositionArea.TopSpanLeft
            or PositionArea.TopSpanRight
            or PositionArea.TopLeft
            or PositionArea.TopRight
            or PositionArea.Top => "top",
            PositionArea.LeftCenter
            or PositionArea.LeftSpanTop
            or PositionArea.LeftSpanBottom
            or PositionArea.Left => "left",
            PositionArea.RightCenter
            or PositionArea.RightSpanTop
            or PositionArea.RightSpanBottom
            or PositionArea.Right => "right",
            _ => "bottom",
        };

    /// <summary>Renders a Lucide icon (via <see cref="IconTagHelper" />) as inline content.</summary>
    public static IHtmlContent RenderIcon(
        TagHelperContext context,
        ThemeManager themeManager,
        ICssClassMerger classMerger,
        IIconManager iconManager,
        string name,
        string cssClass
    )
    {
        var iconOutput = new TagHelperOutput(
            string.Empty,
            [new TagHelperAttribute("class", cssClass)],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
        );
        var iconTagHelper = new IconTagHelper(themeManager, classMerger, iconManager)
        {
            Name = name,
        };
        iconTagHelper.Process(context, iconOutput);

        return iconOutput;
    }
}
