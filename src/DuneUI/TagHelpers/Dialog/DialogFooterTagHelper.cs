using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     The footer region of a dialog; typically contains action buttons.
/// </summary>
[HtmlTargetElement("dui-dialog-footer")]
public class DialogFooterTagHelper : DuneUITagHelperBase
{
    public DialogFooterTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.Add("data-slot", "dialog-footer");
        output.Attributes.Add(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-dialog-footer"),
                "flex flex-col-reverse gap-2 sm:flex-row sm:justify-end",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
