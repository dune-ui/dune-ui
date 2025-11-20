using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sa-item-media")]
public class ItemMediaTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    private const string BaseClasses =
        "flex shrink-0 items-center justify-center gap-2 group-has-[[data-slot=item-description]]/item:self-start [&_svg]:pointer-events-none group-has-[[data-slot=item-description]]/item:translate-y-0.5";
    private static readonly Dictionary<ItemMediaVariant, string> ItemVariantClasses = new()
    {
        [ItemMediaVariant.Default] = "bg-transparent",
        [ItemMediaVariant.Icon] =
            "size-8 border rounded-sm bg-muted [&_svg:not([class*='size-'])]:size-4",
        [ItemMediaVariant.Image] =
            "size-10 rounded-sm overflow-hidden [&_img]:size-full [&_img]:object-cover",
    };

    [HtmlAttributeName("variant")]
    public ItemMediaVariant Variant { get; set; } = ItemMediaVariant.Default;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                BaseClasses,
                ItemVariantClasses[Variant],
                GetUserSpecifiedClass(output)
            )
        );
        output.Attributes.SetAttribute("data-slot", "item-media");
        output.Attributes.SetAttribute("data-variant", Variant);

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
