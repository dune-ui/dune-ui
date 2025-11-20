using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sa-progress")]
public class ProgressTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    [HtmlAttributeName("maximum")]
    public int Maximum { get; set; } = 100;

    [HtmlAttributeName("minimum")]
    public int Minimum { get; set; } = 0;

    [HtmlAttributeName("value")]
    public int Value { get; set; } = 0;

    public override void Init(TagHelperContext context)
    {
        base.Init(context);

        if (Minimum >= Maximum)
        {
            throw new ArgumentOutOfRangeException(
                nameof(Minimum),
                "Minimum must be less than Maximum"
            );
        }

        if (Value < Minimum || Value > Maximum)
        {
            throw new ArgumentOutOfRangeException(
                nameof(Value),
                "Must be between Minimum and Maximum"
            );
        }
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "progress");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "bg-primary/20 relative h-2 w-full overflow-hidden rounded-full",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
