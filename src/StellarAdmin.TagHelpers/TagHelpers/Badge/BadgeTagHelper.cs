using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-badge")]
public class BadgeTagHelper : StellarTagHelper
{
    public BadgeTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    private static readonly Dictionary<BadgeVariant, ComponentName> BadgeVariantClasses = new()
    {
        [BadgeVariant.Default] = new ComponentName("dui-badge-variant-default"),
        [BadgeVariant.Secondary] = new ComponentName("dui-badge-variant-secondary"),
        [BadgeVariant.Destructive] = new ComponentName("dui-badge-variant-destructive"),
        [BadgeVariant.Outline] = new ComponentName("dui-badge-variant-outline"),
        [BadgeVariant.Ghost] = new ComponentName("dui-badge-variant-ghost"),
        [BadgeVariant.Link] = new ComponentName("dui-badge-variant-link"),
    };

    [HtmlAttributeName("variant")]
    public BadgeVariant? Variant { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveVariant = Variant ?? BadgeVariant.Default;

        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "badge");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ComponentName("dui-badge"),
                "inline-flex items-center justify-center w-fit whitespace-nowrap shrink-0 [&>svg]:pointer-events-none focus-visible:border-ring focus-visible:ring-ring/50 focus-visible:ring-[3px] aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive transition-colors overflow-hidden group/badge",
                BadgeVariantClasses[effectiveVariant],
                output.GetUserSuppliedClass()
            )
        );

        output.Content.SetHtmlContent(await output.GetChildContentAsync());
    }
}
