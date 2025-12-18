using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-button-group-separator")]
public class ButtonGroupSeparatorTagHelper : StellarTagHelper
{
    public ButtonGroupSeparatorTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    [HtmlAttributeName("orientation")]
    public SeparatorOrientation? Orientation { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveOrientation = Orientation ?? SeparatorOrientation.Vertical;

        output.Attributes.SetAttribute("data-slot", "button-group-separator");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ComponentName("dui-button-group-separator"),
                "relative self-stretch data-[orientation=horizontal]:mx-px data-[orientation=horizontal]:w-auto data-[orientation=vertical]:my-px data-[orientation=vertical]:h-auto",
                output.GetUserSuppliedClass()
            )
        );

        var separatorTagHelper = new SeparatorTagHelper(ThemeManager, ClassMerger)
        {
            Orientation = effectiveOrientation,
        };

        await separatorTagHelper.ProcessAsync(context, output);
    }
}
