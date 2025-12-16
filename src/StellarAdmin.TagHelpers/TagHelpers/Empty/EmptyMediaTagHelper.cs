using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-empty-media")]
public class EmptyMediaTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public EmptyMediaTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    private static readonly Dictionary<EmptyMediaVariant, string> VariantClasses = new Dictionary<
        EmptyMediaVariant,
        string
    >
    {
        [EmptyMediaVariant.Default] = "bg-transparent",
        [EmptyMediaVariant.Icon] =
            "bg-muted text-foreground flex size-10 shrink-0 items-center justify-center rounded-lg [&_svg:not([class*='size-'])]:size-6",
    };

    [HtmlAttributeName("variant")]
    public EmptyMediaVariant Variant { get; set; } = EmptyMediaVariant.Default;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "empty-icon");
        output.Attributes.SetAttribute("data-variant", GetVariantAttributeText());
        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge(
                "flex shrink-0 items-center justify-center mb-2 [&_svg]:pointer-events-none [&_svg]:shrink-0",
                VariantClasses[Variant],
                output.GetUserSuppliedClass()
            )
        );

        output.Content.SetHtmlContent(await output.GetChildContentAsync());
    }

    private string GetVariantAttributeText() =>
        Variant switch
        {
            EmptyMediaVariant.Default => "default",
            EmptyMediaVariant.Icon => "icon",
            _ => string.Empty,
        };
}
