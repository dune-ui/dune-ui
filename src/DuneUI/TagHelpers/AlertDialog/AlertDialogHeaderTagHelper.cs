using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     The header region of an alert dialog; typically contains the title and description.
/// </summary>
[HtmlTargetElement("dui-alert-dialog-header")]
public class AlertDialogHeaderTagHelper : DuneUITagHelperBase
{
    public AlertDialogHeaderTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.Add("data-slot", "alert-dialog-header");
        output.Attributes.Add(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-alert-dialog-header"),
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
