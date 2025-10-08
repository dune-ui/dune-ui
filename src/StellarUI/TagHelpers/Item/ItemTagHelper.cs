using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-item")]
public class ItemTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    private const string BaseClasses =
        "group/item flex items-center border border-transparent text-sm rounded-md transition-colors [a]:hover:bg-accent/50 [a]:transition-colors duration-100 flex-wrap outline-none focus-visible:border-ring focus-visible:ring-ring/50 focus-visible:ring-[3px]";
    private static readonly Dictionary<ItemSize, string> ItemSizeClasses = new()
    {
        [ItemSize.Default] = "p-4 gap-4",
        [ItemSize.Small] = "py-3 px-4 gap-2.5",
    };

    private static readonly Dictionary<ItemVariant, string> ItemVariantClasses = new()
    {
        [ItemVariant.Default] = "bg-transparent",
        [ItemVariant.Outline] = "border-border",
        [ItemVariant.Muted] = "bg-muted/50",
    };

    [HtmlAttributeName("size")]
    public ItemSize Size { get; set; } = ItemSize.Default;

    [HtmlAttributeName("variant")]
    public ItemVariant Variant { get; set; } = ItemVariant.Default;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                BaseClasses,
                ItemSizeClasses[Size],
                ItemVariantClasses[Variant],
                GetUserSpecifiedClass(output)
            )
        );
        output.Attributes.SetAttribute("data-slot", "item");
        output.Attributes.SetAttribute("data-variant", Variant);
        output.Attributes.SetAttribute("data-size", Size);

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
