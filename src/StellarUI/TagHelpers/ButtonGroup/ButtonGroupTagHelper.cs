using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-button-group")]
public class ButtonGroupTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    private static readonly Dictionary<ButtonGroupOrientation, string> OrientationClasses = new()
    {
        [ButtonGroupOrientation.Horizontal] =
            "[&>*:not(:first-child)]:rounded-l-none [&>*:not(:first-child)]:border-l-0 [&>*:not(:last-child)]:rounded-r-none",
        [ButtonGroupOrientation.Vertical] =
            "flex-col [&>*:not(:first-child)]:rounded-t-none [&>*:not(:first-child)]:border-t-0 [&>*:not(:last-child)]:rounded-b-none",
    };

    [HtmlAttributeName("orientation")]
    public ButtonGroupOrientation Orientation { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("role", "group");
        output.Attributes.SetAttribute("data-slot", "button-group");
        output.Attributes.SetAttribute("data-orientation", GetOrientationAttributeText());
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "flex w-fit items-stretch [&>*]:focus-visible:z-10 [&>*]:focus-visible:relative [&>[data-slot=select-trigger]:not([class*='w-'])]:w-fit [&>input]:flex-1 has-[select[aria-hidden=true]:last-child]:[&>[data-slot=select-trigger]:last-of-type]:rounded-r-md has-[>[data-slot=button-group]]:gap-2",
                OrientationClasses[Orientation],
                output.GetUserSuppliedClass()
            )
        );
        return base.ProcessAsync(context, output);
    }

    private string GetOrientationAttributeText() =>
        Orientation switch
        {
            ButtonGroupOrientation.Horizontal => "horizontal",
            ButtonGroupOrientation.Vertical => "vertical",
            _ => string.Empty,
        };
}
