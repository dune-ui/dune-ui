using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-separator")]
public class SeparatorTagHelper : StellarTagHelper
{
    private static readonly Dictionary<SeparatorOrientation, ComponentName> OrientationClasses =
        new()
        {
            [SeparatorOrientation.Horizontal] = new ComponentName("dui-separator-horizontal"),
            [SeparatorOrientation.Vertical] = new ComponentName("dui-separator-vertical"),
        };

    public SeparatorTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    [HtmlAttributeName("orientation")]
    public SeparatorOrientation? Orientation { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveOrientation = Orientation ?? SeparatorOrientation.Horizontal;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("role", "separator");
        output.Attributes.SetAttribute(
            "aria-orientation",
            effectiveOrientation.GetDataAttributeText()
        );
        output.Attributes.Add("data-slot", "separator");
        output.Attributes.SetAttribute(
            "data-orientation",
            effectiveOrientation.GetDataAttributeText()
        );

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-separator"),
                OrientationClasses[effectiveOrientation],
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
