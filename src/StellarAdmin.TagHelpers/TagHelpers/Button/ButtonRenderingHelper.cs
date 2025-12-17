using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

internal static class ButtonRenderingHelper
{
    private static readonly Dictionary<ButtonVariant, ComponentName> ButtonVariantClasses =
        new Dictionary<ButtonVariant, ComponentName>
        {
            [ButtonVariant.Default] = new ComponentName("dui-button-variant-default"),
            [ButtonVariant.Destructive] = new ComponentName("dui-button-variant-destructive"),
            [ButtonVariant.Outline] = new ComponentName("dui-button-variant-outline"),
            [ButtonVariant.Secondary] = new ComponentName("dui-button-variant-secondary"),
            [ButtonVariant.Ghost] = new ComponentName("dui-button-variant-ghost"),
            [ButtonVariant.Link] = new ComponentName("dui-button-variant-link"),
        };

    private static readonly Dictionary<ButtonSize, ComponentName> ButtonSizeClasses =
        new Dictionary<ButtonSize, ComponentName>
        {
            [ButtonSize.Default] = new ComponentName("dui-button-size-default"),
            [ButtonSize.ExtraSmall] = new ComponentName("dui-button-size-xs"),
            [ButtonSize.Small] = new ComponentName("dui-button-size-sm"),
            [ButtonSize.Large] = new ComponentName("dui-button-size-lg"),
            [ButtonSize.Icon] = new ComponentName("dui-button-size-icon"),
            [ButtonSize.IconExtraSmall] = new ComponentName("dui-button-size-icon-xs"),
            [ButtonSize.IconSmall] = new ComponentName("dui-button-size-icon-sm"),
            [ButtonSize.IconLarge] = new ComponentName("dui-button-size-icon-lg"),
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
                new ComponentName("dui-button"),
                "inline-flex items-center justify-center whitespace-nowrap  transition-all disabled:pointer-events-none disabled:opacity-50 [&_svg]:pointer-events-none shrink-0 [&_svg]:shrink-0 outline-none group/button select-none",
                ButtonVariantClasses[variant],
                ButtonSizeClasses[size],
                output.GetUserSuppliedClass()
            )
        );
    }
}
