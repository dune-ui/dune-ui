using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-button-group-text")]
public class ButtonGroupTextTagHelper : StellarTagHelper
{
    public ButtonGroupTextTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "button-group-text");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ComponentName("dui-button-group-text"),
                "flex items-center [&_svg]:pointer-events-none",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
