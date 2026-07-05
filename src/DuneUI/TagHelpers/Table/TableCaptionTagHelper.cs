using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A caption for a table, rendered as a <c>&lt;caption&gt;</c>; describes the table's
///     contents.
/// </summary>
[HtmlTargetElement("dui-table-caption")]
public class TableCaptionTagHelper : DuneUITagHelperBase
{
    public TableCaptionTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "caption";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "table-caption");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(new ThemeToken("dui-table-caption"), output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
