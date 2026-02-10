using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-popover")]
public class PopoverTagHelper : DuneUITagHelperBase
{
    [HtmlAttributeName("default-open")]
    public bool? DefaultOpen { get; set; }

    [HtmlAttributeName("open-on-hover")]
    public bool? OpenOnHover { get; set; }

    [HtmlAttributeName("popup-align")]
    public PopupAlign? PopupAlign { get; set; }

    [HtmlAttributeName("popup-offset")]
    public int? PopupOffset { get; set; }

    [HtmlAttributeName("popup-side")]
    public PopupSide? PopupSide { get; set; }

    public PopoverTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveDefaultOpen = DefaultOpen ?? false;
        var effectiveOpenOnHover = OpenOnHover ?? false;
        var effectivePopupAlign = PopupAlign ?? TagHelpers.PopupAlign.Center;
        var effectivePopupOffset = PopupOffset ?? 4;
        var effectivePopupSide = PopupSide ?? TagHelpers.PopupSide.Bottom;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "x-data",
            AlpineJsDataSerializer.SerializeDataFunction(
                "popover",
                [
                    effectiveOpenOnHover,
                    effectiveDefaultOpen,
                    AlpineJsDataSerializer.SerializePopupPosition(
                        effectivePopupSide,
                        effectivePopupAlign
                    ),
                    effectivePopupOffset,
                ]
            )
        );

        output.Attributes.SetAttribute("data-slot", "popover");

        return Task.CompletedTask;
    }
}
