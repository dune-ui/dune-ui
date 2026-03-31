using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-input-group-button")]
public class InputGroupButtonTagHelper : DuneUITagHelperBase
{
    private static readonly Dictionary<InputGroupButtonSize, ThemeToken> SizeClasses =
        new Dictionary<InputGroupButtonSize, ThemeToken>
        {
            [InputGroupButtonSize.ExtraSmall] = new ThemeToken("dui-input-group-button-size-xs"),
            [InputGroupButtonSize.Small] = new ThemeToken("dui-input-group-button-size-sm"),
            [InputGroupButtonSize.IconExtraSmall] = new ThemeToken(
                "dui-input-group-button-size-icon-xs"
            ),
            [InputGroupButtonSize.IconSmall] = new ThemeToken(
                "dui-input-group-button-size-icon-sm"
            ),
        };

    [HtmlAttributeName("size")]
    public InputGroupButtonSize? Size { get; set; }

    [HtmlAttributeName("variant")]
    public ButtonVariant? Variant { get; set; }

    public InputGroupButtonTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveSize = Size ?? InputGroupButtonSize.ExtraSmall;

        output.TagName = "button";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-size", effectiveSize.GetDataAttributeText());
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-input-group-button"),
                "shadow-none flex items-center",
                SizeClasses[effectiveSize],
                output.GetUserSuppliedClass()
            )
        );

        ButtonRenderingHelper.RenderAttributes(
            output,
            ClassMerger,
            Variant ?? ButtonVariant.Ghost,
            ButtonSize.Default
        );

        return base.ProcessAsync(context, output);
    }
}
