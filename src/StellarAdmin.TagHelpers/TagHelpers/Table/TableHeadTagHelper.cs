using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-table-head")]
public class TableHeadTagHelper : StellarTagHelper
{
    public TableHeadTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "th";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "table-head");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(new ComponentName("dui-table-head"), output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
