using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A data cell within a table row, rendered as a <c>&lt;td&gt;</c>.
/// </summary>
[HtmlTargetElement("dui-table-cell")]
public class TableCellTagHelper : DuneUITagHelperBase
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
            ClassMerger.Merge(new ThemeToken("dui-table-cell"), output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
