using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A small label used to highlight status, counts, or categories.
/// </summary>
[HtmlTargetElement("dui-badge")]
public class BadgeTagHelper : DuneUITagHelperBase
{
    public BadgeTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    private static readonly Dictionary<BadgeVariant, ThemeToken> BadgeVariantClasses = new()
    {
        [BadgeVariant.Default] = new ThemeToken("dui-badge-variant-default"),
        [BadgeVariant.Secondary] = new ThemeToken("dui-badge-variant-secondary"),
        [BadgeVariant.Destructive] = new ThemeToken("dui-badge-variant-destructive"),
        [BadgeVariant.Outline] = new ThemeToken("dui-badge-variant-outline"),
        [BadgeVariant.Ghost] = new ThemeToken("dui-badge-variant-ghost"),
        [BadgeVariant.Link] = new ThemeToken("dui-badge-variant-link"),
    };

    /// <summary>
    ///     The visual style of the badge.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="BadgeVariant.Default" />.
    /// </remarks>
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
                new ThemeToken("dui-badge"),
                "inline-flex items-center justify-center w-fit whitespace-nowrap shrink-0 [&>svg]:pointer-events-none focus-visible:border-ring focus-visible:ring-ring/50 focus-visible:ring-[3px] aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive transition-colors overflow-hidden group/badge",
                BadgeVariantClasses[effectiveVariant],
                output.GetUserSuppliedClass()
            )
        );

        output.Content.SetHtmlContent(await output.GetChildContentAsync());
    }
}
