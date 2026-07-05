using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     The leading media region of an item, holding an icon, image, or avatar.
/// </summary>
[HtmlTargetElement("dui-item-media")]
public class ItemMediaTagHelper : DuneUITagHelperBase
{
    private static readonly Dictionary<ItemMediaVariant, string> ItemVariantClasses =
        new Dictionary<ItemMediaVariant, string>
        {
            [ItemMediaVariant.Default] = "dui-item-media-variant-default",
            [ItemMediaVariant.Icon] = "dui-item-media-variant-icon",
            [ItemMediaVariant.Image] = "dui-item-media-variant-image",
        };

    /// <summary>
    ///     The kind of media the region contains, which controls its sizing and styling.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="ItemMediaVariant.Default" />.
    /// </remarks>
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
                new ThemeToken("dui-item-media"),
                "flex shrink-0 items-center justify-center [&_svg]:pointer-events-none",
                ItemVariantClasses[effectiveVariant],
                GetUserSpecifiedClass(output)
            )
        );

        return Task.CompletedTask;
    }
}
