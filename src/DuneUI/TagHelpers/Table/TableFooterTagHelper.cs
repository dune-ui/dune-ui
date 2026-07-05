using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     The footer of a table, rendered as a <c>&lt;tfoot&gt;</c>; typically holds summary rows.
/// </summary>
[HtmlTargetElement("dui-table-footer")]
public class TableFooterTagHelper : DuneUITagHelperBase
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
            ClassMerger.Merge(new ThemeToken("dui-table-footer"), output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
