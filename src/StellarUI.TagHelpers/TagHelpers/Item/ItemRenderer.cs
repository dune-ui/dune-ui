using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

internal class ItemRenderer(ICssClassMerger classMerger)
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

    public async Task RenderAsync(
        TagHelperContext context,
        TagHelperOutput output,
        ItemSize size,
        ItemVariant variant
    )
    {
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                BaseClasses,
                ItemSizeClasses[size],
                ItemVariantClasses[variant],
                output.GetUserSuppliedClass()
            )
        );
        output.Attributes.SetAttribute("data-slot", "item");
        output.Attributes.SetAttribute("data-variant", GetVariantAttributeText(variant));
        output.Attributes.SetAttribute("data-size", GetSizeAttributeText(size));

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }

    private string GetSizeAttributeText(ItemSize size)
    {
        return size switch
        {
            ItemSize.Default => "default",
            ItemSize.Small => "sm",
            _ => string.Empty,
        };
    }

    private string GetVariantAttributeText(ItemVariant variant)
    {
        return variant switch
        {
            ItemVariant.Default => "default",
            ItemVariant.Outline => "outline",
            ItemVariant.Muted => "muted",
            _ => string.Empty,
        };
    }
}
