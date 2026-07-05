using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     The descriptive body text of a dialog, shown beneath the title.
/// </summary>
[HtmlTargetElement("dui-dialog-description")]
public class DialogDescriptionTagHelper : DuneUITagHelperBase
{
    public DialogDescriptionTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "p";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.Add("data-slot", "dialog-description");
        output.Attributes.Add(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-dialog-description"),
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
