using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-progress-label")]
public class ProgressLabelTagHelper : StellarTagHelper
{
    public ProgressLabelTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.Add("data-slot", "progress-label");
        output.Attributes.Add(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-progress-label"),
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
