using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-table-header")]
public class TableHeaderTagHelper : StellarTagHelper
{
    public TableHeaderTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "thead";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "table-header");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(new ComponentName("dui-table-header"), output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
