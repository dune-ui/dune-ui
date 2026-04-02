using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers.Sheet;

[HtmlTargetElement("dui-sheet-title")]
public class SheetTitleTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "h2";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.Add("data-slot", "sheet-title");
        output.Attributes.Add(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-sheet-title"),
                new ThemeToken("dui-font-heading"),
                "font-heading",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
