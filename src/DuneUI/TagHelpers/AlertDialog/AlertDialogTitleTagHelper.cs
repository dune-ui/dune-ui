using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-alert-dialog-title")]
public class AlertDialogTitleTagHelper : DuneUITagHelperBase
{
    public AlertDialogTitleTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "h2";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.Add("data-slot", "alert-dialog-title");
        output.Attributes.Add(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-alert-dialog-title"),
                new ThemeToken("dui-font-heading"),
                "font-heading",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
