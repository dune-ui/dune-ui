using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A header cell within a table header row, rendered as a <c>&lt;th&gt;</c>.
/// </summary>
[HtmlTargetElement("dui-table-head")]
public class TableHeadTagHelper : DuneUITagHelperBase
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
            ClassMerger.Merge(new ThemeToken("dui-table-head"), output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
