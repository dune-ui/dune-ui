using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-field-legend")]
public class FieldLegendTagHelper : StellarTagHelper
{
    public FieldLegendTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

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
            BuildClassString(new ComponentName("dui-field-legend"), output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
