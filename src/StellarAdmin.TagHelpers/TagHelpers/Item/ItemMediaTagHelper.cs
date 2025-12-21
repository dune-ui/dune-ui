using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-item-media")]
public class ItemMediaTagHelper : StellarTagHelper
{
    private static readonly Dictionary<ItemMediaVariant, string> ItemVariantClasses =
        new Dictionary<ItemMediaVariant, string>
        {
            [ItemMediaVariant.Default] = "dui-item-media-variant-default",
            [ItemMediaVariant.Icon] = "dui-item-media-variant-icon",
            [ItemMediaVariant.Image] = "dui-item-media-variant-image",
        };

    [HtmlAttributeName("variant")]
    public ItemMediaVariant? Variant { get; set; }

    public ItemMediaTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveVariant = Variant ?? ItemMediaVariant.Default;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "item-media");
        output.Attributes.SetAttribute("data-variant", effectiveVariant.GetDataAttributeText());
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-item-media"),
                "flex shrink-0 items-center justify-center [&_svg]:pointer-events-none",
                ItemVariantClasses[effectiveVariant],
                GetUserSpecifiedClass(output)
            )
        );

        return Task.CompletedTask;
    }
}
