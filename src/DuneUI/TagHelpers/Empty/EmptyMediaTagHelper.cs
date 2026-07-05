using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     The media region of an empty state, displaying an icon or illustration above the title.
/// </summary>
[HtmlTargetElement("dui-empty-media")]
public class EmptyMediaTagHelper : DuneUITagHelperBase
{
    private static readonly Dictionary<EmptyMediaVariant, ThemeToken> VariantClasses =
        new Dictionary<EmptyMediaVariant, ThemeToken>
        {
            [EmptyMediaVariant.Default] = new ThemeToken("dui-empty-media-default"),
            [EmptyMediaVariant.Icon] = new ThemeToken("dui-empty-media-icon"),
        };

    /// <summary>
    ///     The visual style of the media region.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="EmptyMediaVariant.Default" />.
    /// </remarks>
    [HtmlAttributeName("variant")]
    public EmptyMediaVariant? Variant { get; set; }

    public EmptyMediaTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveVariant = Variant ?? EmptyMediaVariant.Default;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "empty-icon");
        output.Attributes.SetAttribute("data-variant", effectiveVariant.GetDataAttributeText());
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-empty-media"),
                "flex shrink-0 items-center justify-center [&_svg]:pointer-events-none [&_svg]:shrink-0",
                VariantClasses[effectiveVariant],
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
