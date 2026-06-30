using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-alert-dialog-footer")]
public class AlertDialogFooterTagHelper : DuneUITagHelperBase
{
    public AlertDialogFooterTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.Add("data-slot", "alert-dialog-footer");
        output.Attributes.Add(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-alert-dialog-footer"),
                "flex flex-col-reverse gap-2 group-data-[size=sm]/alert-dialog-content:grid group-data-[size=sm]/alert-dialog-content:grid-cols-2 sm:flex-row sm:justify-end",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
