using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/*
 * Tooltip uses the interest invokers. Here are various links I found useful while developing this:
 *
 * - https://open-ui.org/components/interest-invokers.explainer/
 * - https://chrome.dev/anchor-tool/
 * - https://css-tricks.com/css-anchor-positioning-guide/
 * - https://github.com/toolwind/anchors?tab=readme-ov-file
 * - https://developer.chrome.com/blog/popover-hint
 * - https://developer.chrome.com/blog/new-in-web-ui-io-2025-recap#css_anchor_positioning
 * - https://codepen.io/una/pen/JooENdE
 * - https://github.com/mfreed7/interestfor/tree/main?tab=readme-ov-file
 */

[HtmlTargetElement("dui-tooltip")]
public class TooltipTagHelper : DuneUITagHelperBase
{
    [HtmlAttributeName("position")]
    public PositionArea? Position { get; set; }

    public TooltipTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        var effectivePositionArea = Position ?? PositionArea.Top;

        if (!output.Attributes.ContainsName("popover"))
        {
            output.Attributes.SetAttribute("popover", "hint");
        }

        output.Attributes.SetAttribute("data-slot", "tooltip-content");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-tooltip-content"),
                "w-fit max-w-xs origin-(--transform-origin) bg-foreground text-background",
                "try-flip-all m-2",
                effectivePositionArea.GetTailwindClassName(),
                "duration-200 ease-in opacity-100 not-open:opacity-0 starting:open:opacity-0 [transition-property:opacity,display,overlay] [transition-behavior:allow-discrete]",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
