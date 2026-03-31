using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-popover")]
public class PopoverTagHelper : DuneUITagHelperBase
{
    [HtmlAttributeName("position")]
    public PositionArea? Position { get; set; }

    public PopoverTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        var effectivePositionArea = Position ?? PositionArea.Bottom;

        if (!output.Attributes.ContainsName("popover"))
        {
            output.Attributes.SetAttribute("popover", "");
        }

        output.Attributes.SetAttribute("data-slot", "popover-content");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-popover-content"),
                "w-72 outline-hidden",
                "try-flip-all",
                effectivePositionArea.GetTailwindClassName(),
                GetMarginClassName(effectivePositionArea),
                "duration-200 ease-in opacity-100 not-open:opacity-0 starting:open:opacity-0 [transition-property:opacity,display,overlay] [transition-behavior:allow-discrete]",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }

    private string GetMarginClassName(PositionArea positionArea)
    {
        return positionArea switch
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
    }
}
