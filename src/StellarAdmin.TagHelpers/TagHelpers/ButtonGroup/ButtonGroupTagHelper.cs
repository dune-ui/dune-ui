using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-button-group")]
public class ButtonGroupTagHelper : StellarTagHelper
{
    private static readonly Dictionary<ButtonGroupOrientation, ClassElement[]> OrientationClasses =
        new Dictionary<ButtonGroupOrientation, ClassElement[]>
        {
            [ButtonGroupOrientation.Horizontal] =
            [
                new ComponentName("dui-button-group-orientation-horizontal"),
                "[&>[data-slot]~[data-slot]]:rounded-l-none [&>[data-slot]~[data-slot]]:border-l-0 [&>[data-slot]]:rounded-r-none",
            ],
            [ButtonGroupOrientation.Vertical] =
            [
                new ComponentName("dui-button-group-orientation-vertical"),
                "flex-col [&>[data-slot]~[data-slot]]:rounded-t-none [&>[data-slot]~[data-slot]]:border-t-0 [&>[data-slot]]:rounded-b-none",
            ],
        };

    [HtmlAttributeName("orientation")]
    public ButtonGroupOrientation? Orientation { get; set; }

    public ButtonGroupTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveOrientation = Orientation ?? ButtonGroupOrientation.Horizontal;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("role", "group");
        output.Attributes.SetAttribute("data-slot", "button-group");
        output.Attributes.SetAttribute(
            "data-orientation",
            effectiveOrientation.GetDataAttributeText()
        );
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ClassElement[]
                {
                    new ComponentName("dui-button-group"),
                    "flex w-fit items-stretch [&>*]:focus-visible:z-10 [&>*]:focus-visible:relative [&>[data-slot=select-trigger]:not([class*='w-'])]:w-fit [&>input]:flex-1",
                }
                    .Union(OrientationClasses[effectiveOrientation])
                    .Union([output.GetUserSuppliedClass()])
                    .ToArray()
            )
        );
        return base.ProcessAsync(context, output);
    }
}
