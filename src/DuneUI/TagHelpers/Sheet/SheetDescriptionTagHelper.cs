using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers.Sheet;

/// <summary>
///     Supporting description text for a sheet, shown beneath the title.
/// </summary>
[HtmlTargetElement("dui-sheet-description")]
public class SheetDescriptionTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "p";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.Add("data-slot", "sheet-description");
        output.Attributes.Add(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-sheet-description"),
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
