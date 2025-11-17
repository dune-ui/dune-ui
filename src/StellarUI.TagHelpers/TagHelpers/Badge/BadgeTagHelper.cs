using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-badge")]
public class BadgeTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    private static readonly Dictionary<BadgeVariant, string> BadgeVariantClasses = new()
    {
        [BadgeVariant.Default] =
            "border-transparent bg-primary text-primary-foreground [a&]:hover:bg-primary/90",
        [BadgeVariant.Secondary] =
            "border-transparent bg-secondary text-secondary-foreground [a&]:hover:bg-secondary/90",
        [BadgeVariant.Destructive] =
            "border-transparent bg-destructive text-white [a&]:hover:bg-destructive/90 focus-visible:ring-destructive/20 dark:focus-visible:ring-destructive/40 dark:bg-destructive/60",
        [BadgeVariant.Outline] =
            "text-foreground [a&]:hover:bg-accent [a&]:hover:text-accent-foreground",
    };

    [HtmlAttributeName("variant")]
    public BadgeVariant Variant { get; set; } = BadgeVariant.Default;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "badge");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "inline-flex items-center justify-center rounded-md border px-2 py-0.5 text-xs font-medium w-fit whitespace-nowrap shrink-0 [&>svg]:size-3 gap-1 [&>svg]:pointer-events-none focus-visible:border-ring focus-visible:ring-ring/50 focus-visible:ring-[3px] aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive transition-[color,box-shadow] overflow-hidden",
                BadgeVariantClasses[Variant],
                output.GetUserSuppliedClass()
            )
        );

        output.Content.SetHtmlContent(await output.GetChildContentAsync());
    }
}
