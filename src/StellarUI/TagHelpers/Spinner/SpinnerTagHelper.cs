using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-spinner")]
public class SpinnerTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var iconRenderer = new IconRenderer();
        iconRenderer.Render(output, "loader-circle");

        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge("animate-spin", output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
