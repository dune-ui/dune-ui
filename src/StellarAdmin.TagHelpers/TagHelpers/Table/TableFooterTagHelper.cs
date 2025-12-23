using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-table-footer")]
public class TableFooterTagHelper : StellarTagHelper
{
    public TableFooterTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "tfoot";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "table-footer");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(new ComponentName("dui-table-footer"), output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
