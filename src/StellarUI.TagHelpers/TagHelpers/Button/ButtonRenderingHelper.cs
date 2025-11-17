using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

internal static class ButtonRenderingHelper
{
    private static readonly Dictionary<ButtonVariant, string> ButtonVariantClasses = new Dictionary<
        ButtonVariant,
        string
    >
    {
        [ButtonVariant.Default] = "bg-primary text-primary-foreground hover:bg-primary/90",
        [ButtonVariant.Destructive] =
            "bg-destructive text-white hover:bg-destructive/90 focus-visible:ring-destructive/20 dark:focus-visible:ring-destructive/40 dark:bg-destructive/60",
        [ButtonVariant.Outline] =
            "border bg-background shadow-xs hover:bg-accent hover:text-accent-foreground dark:bg-input/30 dark:border-input dark:hover:bg-input/50",
        [ButtonVariant.Secondary] = "bg-secondary text-secondary-foreground hover:bg-secondary/80",
        [ButtonVariant.Ghost] =
            "hover:bg-accent hover:text-accent-foreground dark:hover:bg-accent/50",
        [ButtonVariant.Link] = "text-primary underline-offset-4 hover:underline",
    };

    private static readonly Dictionary<ButtonSize, string> ButtonSizeClasses = new Dictionary<
        ButtonSize,
        string
    >
    {
        [ButtonSize.Default] = "h-9 px-4 py-2 has-[>svg]:px-3",
        [ButtonSize.Small] = "h-8 rounded-md gap-1.5 px-3 has-[>svg]:px-2.5",
        [ButtonSize.Large] = "h-10 rounded-md px-6 has-[>svg]:px-4",
        [ButtonSize.Icon] = "size-9",
        [ButtonSize.IconSmall] = "size-8",
        [ButtonSize.IconLarge] = "size-10",
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
                "inline-flex items-center justify-center gap-2 whitespace-nowrap rounded-md text-sm font-medium transition-all disabled:pointer-events-none disabled:opacity-50 [&_svg]:pointer-events-none [&_svg:not([class*='size-'])]:size-4 shrink-0 [&_svg]:shrink-0 outline-none focus-visible:border-ring focus-visible:ring-ring/50 focus-visible:ring-[3px] aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive",
                ButtonVariantClasses[variant],
                ButtonSizeClasses[size],
                output.GetUserSuppliedClass()
            )
        );
    }
}
