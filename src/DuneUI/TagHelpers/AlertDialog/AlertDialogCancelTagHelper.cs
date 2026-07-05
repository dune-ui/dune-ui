using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     The dismissing button of an alert dialog. Renders a styled outline submit button with
///     <c>value="cancel"</c> so a wrapping <c>&lt;form method="dialog"&gt;</c> closes the dialog
///     with that <c>returnValue</c> (which the <c>duneui.alertDialog</c> helper reads as
///     <c>confirmed: false</c>).
/// </summary>
[HtmlTargetElement("dui-alert-dialog-cancel")]
public class AlertDialogCancelTagHelper : DuneUITagHelperBase
{
    /// <summary>
    ///     The visual style of the cancel button.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="ButtonVariant.Outline" />.
    /// </remarks>
    [HtmlAttributeName("variant")]
    public ButtonVariant? Variant { get; set; }

    public AlertDialogCancelTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveVariant = Variant ?? ButtonVariant.Outline;

        output.TagName = "button";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("type", "submit");
        if (!output.Attributes.ContainsName("value"))
        {
            output.Attributes.SetAttribute("value", "cancel");
        }

        // Override Button's data-slot and fold in the alert-dialog-cancel token
        // *after* the button classes (RenderAttributes folds GetUserSuppliedClass last).
        output.Attributes.SetAttribute("data-slot", "alert-dialog-cancel");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-alert-dialog-cancel"),
                output.GetUserSuppliedClass()
            )
        );

        ButtonRenderingHelper.RenderAttributes(
            output,
            ClassMerger,
            effectiveVariant,
            ButtonSize.Default
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
