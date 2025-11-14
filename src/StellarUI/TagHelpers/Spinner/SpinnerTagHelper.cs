using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-spinner")]
public class SpinnerTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var iconTagHelper = new IconTagHelper { Name = "loader-circle" };
        await iconTagHelper.ProcessAsync(context, output);

        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge("animate-spin", output.GetUserSuppliedClass())
        );
    }
}
