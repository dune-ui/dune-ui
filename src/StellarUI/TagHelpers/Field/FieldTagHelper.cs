using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-field")]
public class FieldTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    [HtmlAttributeName("orientation")]
    public FieldOrientation Orientation { get; set; } = FieldOrientation.Vertical;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var fieldRenderer = new FieldRenderer(classMerger);
        await fieldRenderer.Render(
            output,
            Orientation,
            async () => await output.GetChildContentAsync()
        );
    }
}
