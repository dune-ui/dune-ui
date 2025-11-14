using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-button-group-separator")]
public class ButtonGroupSeparatorTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    [HtmlAttributeName("orientation")]
    public SeparatorOrientation Orientation { get; set; } = SeparatorOrientation.Vertical;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.SetAttribute("data-slot", "button-group-separator");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "bg-input relative !m-0 self-stretch data-[orientation=vertical]:h-auto",
                output.GetUserSuppliedClass()
            )
        );

        var separatorTagHelper = new SeparatorTagHelper(classMerger) { Orientation = Orientation };
        await separatorTagHelper.ProcessAsync(context, output);
    }
}
