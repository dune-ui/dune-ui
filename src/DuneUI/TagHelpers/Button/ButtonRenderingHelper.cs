using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

internal static class ButtonRenderingHelper
{
    private static readonly Dictionary<ButtonVariant, ThemeToken> ButtonVariantClasses =
        new Dictionary<ButtonVariant, ThemeToken>
        {
            [ButtonVariant.Default] = new ThemeToken("dui-button-variant-default"),
            [ButtonVariant.Destructive] = new ThemeToken("dui-button-variant-destructive"),
            [ButtonVariant.Outline] = new ThemeToken("dui-button-variant-outline"),
            [ButtonVariant.Secondary] = new ThemeToken("dui-button-variant-secondary"),
            [ButtonVariant.Ghost] = new ThemeToken("dui-button-variant-ghost"),
            [ButtonVariant.Link] = new ThemeToken("dui-button-variant-link"),
        };

    private static readonly Dictionary<ButtonSize, ThemeToken> ButtonSizeClasses =
        new Dictionary<ButtonSize, ThemeToken>
        {
            [ButtonSize.Default] = new ThemeToken("dui-button-size-default"),
            [ButtonSize.ExtraSmall] = new ThemeToken("dui-button-size-xs"),
            [ButtonSize.Small] = new ThemeToken("dui-button-size-sm"),
            [ButtonSize.Large] = new ThemeToken("dui-button-size-lg"),
            [ButtonSize.Icon] = new ThemeToken("dui-button-size-icon"),
            [ButtonSize.IconExtraSmall] = new ThemeToken("dui-button-size-icon-xs"),
            [ButtonSize.IconSmall] = new ThemeToken("dui-button-size-icon-sm"),
            [ButtonSize.IconLarge] = new ThemeToken("dui-button-size-icon-lg"),
        };

    public static void RenderAttributes(
        TagHelperOutput output,
        ICssClassMerger classMerger,
        ButtonVariant variant,
        ButtonSize size
    )
    {
        if (!output.Attributes.ContainsName("data-slot"))
        {
            output.Attributes.SetAttribute("data-slot", "button");
        }

        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                new ThemeToken("dui-button"),
                "inline-flex items-center justify-center whitespace-nowrap  transition-all disabled:pointer-events-none disabled:opacity-50 [&_svg]:pointer-events-none shrink-0 [&_svg]:shrink-0 outline-none group/button select-none",
                ButtonVariantClasses[variant],
                ButtonSizeClasses[size],
                output.GetUserSuppliedClass()
            )
        );
    }
}
