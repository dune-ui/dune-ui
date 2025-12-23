using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-table-body")]
public class TableBodyTagHelper : StellarTagHelper
{
    public TableBodyTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "tbody";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "table-body");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(new ComponentName("dui-table-body"), output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
