using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-separator")]
public class SeparatorTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    [HtmlAttributeName("is-decorative")]
    public bool IsDecorative { get; set; } = true;

    [HtmlAttributeName("orientation")]
    public SeparatorOrientation Orientation { get; set; } = SeparatorOrientation.Horizontal;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var separatorRenderer = new SeparatorRenderer(classMerger);

        await separatorRenderer.Render(output, Orientation, IsDecorative);
    }
}
