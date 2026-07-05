using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     The caption for a field set, rendered as a <c>&lt;legend&gt;</c> element.
/// </summary>
[HtmlTargetElement("dui-field-legend")]
public class FieldLegendTagHelper : DuneUITagHelperBase
{
    public FieldLegendTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    /// <summary>
    ///     The visual style applied to the legend.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="FieldLegendVariant.Legend" />.
    /// </remarks>
    [HtmlAttributeName("variant")]
    public FieldLegendVariant? Variant { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveVariant = Variant ?? FieldLegendVariant.Legend;

        output.TagName = "legend";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "field-legend");
        output.Attributes.SetAttribute("data-variant", effectiveVariant.GetDataAttributeText());
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(new ThemeToken("dui-field-legend"), output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
