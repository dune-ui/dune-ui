using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Icons;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-spinner")]
public class SpinnerTagHelper(ICssClassMerger classMerger, IIconManager iconManager)
    : StellarTagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var iconTagHelper = new IconTagHelper(iconManager) { Name = "loader-circle" };
        await iconTagHelper.ProcessAsync(context, output);

        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge("animate-spin", output.GetUserSuppliedClass())
        );
    }
}
