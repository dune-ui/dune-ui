using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-table-cell")]
public class TableCellTagHelper : StellarTagHelper
{
    public TableCellTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "td";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "table-cell");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(new ComponentName("dui-table-cell"), output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
