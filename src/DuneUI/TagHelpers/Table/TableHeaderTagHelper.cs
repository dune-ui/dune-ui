using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     The header section of a table, rendered as a <c>&lt;thead&gt;</c>; contains the header row.
/// </summary>
[HtmlTargetElement("dui-table-header")]
public class TableHeaderTagHelper : DuneUITagHelperBase
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
            ClassMerger.Merge(new ThemeToken("dui-table-header"), output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
