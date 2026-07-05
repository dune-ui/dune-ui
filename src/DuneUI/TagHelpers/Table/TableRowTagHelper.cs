using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A row within a table, rendered as a <c>&lt;tr&gt;</c>.
/// </summary>
[HtmlTargetElement("dui-table-row")]
public class TableRowTagHelper : DuneUITagHelperBase
{
    public TableRowTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "tr";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "table-row");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(new ThemeToken("dui-table-row"), output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
