using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     The confirming button of an alert dialog. Renders a styled submit button with
///     <c>value="confirm"</c> so a wrapping <c>&lt;form method="dialog"&gt;</c> closes the dialog
///     with that <c>returnValue</c> (which the <c>duneui.alertDialog</c> helper reads as
///     <c>confirmed: true</c>). Override <c>variant</c> for a destructive action.
/// </summary>
[HtmlTargetElement("dui-alert-dialog-action")]
public class AlertDialogActionTagHelper : DuneUITagHelperBase
{
    [HtmlAttributeName("variant")]
    public ButtonVariant? Variant { get; set; }

    public AlertDialogActionTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveVariant = Variant ?? ButtonVariant.Default;

        output.TagName = "button";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("type", "submit");
        if (!output.Attributes.ContainsName("value"))
        {
            output.Attributes.SetAttribute("value", "confirm");
        }

        // Match shadcn: override Button's data-slot and fold in the alert-dialog-action token
        // *after* the button classes (RenderAttributes folds GetUserSuppliedClass last).
        output.Attributes.SetAttribute("data-slot", "alert-dialog-action");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-alert-dialog-action"),
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
