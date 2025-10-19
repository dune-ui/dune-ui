using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-field-legend")]
public class FieldLegendTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    [HtmlAttributeName("variant")]
    public FieldLegendVariant Variant { get; set; } = FieldLegendVariant.Legend;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "legend";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "field-legend");
        output.Attributes.SetAttribute("data-variant", GetVariantAttributeText());
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "mb-3 font-medium",
                "data-[variant=legend]:text-base",
                "data-[variant=label]:text-sm",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }

    private string GetVariantAttributeText() =>
        Variant switch
        {
            FieldLegendVariant.Legend => "legend",
            FieldLegendVariant.Label => "label",
            _ => string.Empty,
        };
}
