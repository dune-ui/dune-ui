using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-input-group-text")]
public class InputGroupTextTagHelper : StellarTagHelper
{
    public InputGroupTextTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-input-group-text"),
                "flex items-center [&_svg]:pointer-events-none",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
