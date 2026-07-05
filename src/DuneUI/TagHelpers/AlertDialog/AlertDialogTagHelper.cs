using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A modal dialog that interrupts the user to confirm an important action, rendered over a
///     native <c>&lt;dialog&gt;</c> element. Open and close it with the Invoker Commands API — a
///     trigger button carrying <c>commandfor</c> and <c>command="show-modal"</c> or
///     <c>command="close"</c>. Unlike a regular dialog, it is not dismissed by clicking the
///     backdrop.
/// </summary>
[HtmlTargetElement("dui-alert-dialog")]
public class AlertDialogTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    /// <summary>
    ///     The size of the alert dialog.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="AlertDialogSize.Default" />.
    /// </remarks>
    [HtmlAttributeName("size")]
    public AlertDialogSize? Size { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveSize = Size ?? AlertDialogSize.Default;

        output.TagName = "dialog";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "alert-dialog-content");
        output.Attributes.SetAttribute("data-size", effectiveSize.GetDataAttributeText());

        // Alert dialogs are not light-dismissable: pressing Esc cancels (resolves the
        // JS helper to `confirmed: false`), but clicking the backdrop does not close.
        // `closerequest` is the native value for exactly that; honour an author override.
        if (!output.Attributes.ContainsName("closedby"))
        {
            output.Attributes.SetAttribute("closedby", "closerequest");
        }

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-alert-dialog-content"),
                // `group/alert-dialog-content` establishes the named group the header/title/media
                // tokens react to; `data-open:grid` (not plain `grid`) so the closed dialog keeps
                // the UA `display: none`.
                "group/alert-dialog-content fixed inset-0 m-auto outline-none data-open:grid",
                "backdrop:supports-backdrop-filter:backdrop-blur-xs",
                output.GetUserSuppliedClass()
            )
        );

        // Wrap inside web component (reuses Dialog's del-dialog: scroll-lock + data-open state).
        output.PreElement.AppendHtml("<del-dialog>");
        output.PostElement.AppendHtml("</del-dialog>");

        return Task.CompletedTask;
    }
}
