using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sa-separator")]
public class SeparatorTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    [HtmlAttributeName("is-decorative")]
    public bool IsDecorative { get; set; } = true;

    [HtmlAttributeName("orientation")]
    public SeparatorOrientation Orientation { get; set; } = SeparatorOrientation.Horizontal;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "bg-border shrink-0 data-[orientation=horizontal]:h-px data-[orientation=horizontal]:w-full data-[orientation=vertical]:h-full data-[orientation=vertical]:w-px",
                output.GetUserSuppliedClass()
            )
        );
        output.Attributes.SetAttribute("data-role", IsDecorative ? "none" : "separator");
        output.Attributes.SetAttribute("data-orientation", GetOrientationAttributeText());
    }

    private string GetOrientationAttributeText() =>
        Orientation switch
        {
            SeparatorOrientation.Horizontal => "horizontal",
            SeparatorOrientation.Vertical => "vertical",
            _ => Orientation.ToString(),
        };
}
