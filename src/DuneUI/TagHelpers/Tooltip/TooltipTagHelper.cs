using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-tooltip")]
public class TooltipTagHelper : DuneUITagHelperBase
{
    [HtmlAttributeName("close-delay")]
    public int? CloseDelayDuration { get; set; }

    [HtmlAttributeName("default-open")]
    public bool? DefaultOpen { get; set; }

    [HtmlAttributeName("delay")]
    public int? DelayDuration { get; set; }

    [HtmlAttributeName("tooltip-align")]
    public PopupAlign? TooltipAlign { get; set; }

    [HtmlAttributeName("tooltip-offset")]
    public int? TooltipOffset { get; set; }

    [HtmlAttributeName("tooltip-side")]
    public PopupSide? TooltipSide { get; set; }

    public TooltipTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveDelayDuration = DelayDuration ?? 50;
        var effectiveCloseDelayDuration = CloseDelayDuration ?? 100;
        var effectiveDefaultOpen = DefaultOpen ?? false;
        var effectiveTooltipAlign = TooltipAlign ?? PopupAlign.Center;
        var effectiveTooltipOffset = TooltipOffset ?? 8;
        var effectiveTooltipSide = TooltipSide ?? PopupSide.Top;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "x-data",
            AlpineJsDataSerializer.SerializeDataFunction(
                "tooltip",
                [
                    effectiveDelayDuration,
                    effectiveCloseDelayDuration,
                    effectiveDefaultOpen,
                    AlpineJsDataSerializer.SerializePopupPosition(
                        effectiveTooltipSide,
                        effectiveTooltipAlign
                    ),
                    effectiveTooltipOffset,
                ]
            )
        );

        return Task.CompletedTask;
    }
}
