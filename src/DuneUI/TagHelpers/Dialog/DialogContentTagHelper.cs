using DuneUI.Icons;
using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-dialog-content")]
public class DialogContentTagHelper(
    ThemeManager themeManager,
    ICssClassMerger classMerger,
    IIconManager iconManager
) : DuneUITagHelperBase(themeManager, classMerger)
{
    private readonly IIconManager _iconManager = iconManager;

    [HtmlAttributeName("show-close-button")]
    public bool? ShowCloseButton { get; set; }

    [HtmlAttributeName("teleport")]
    public string? Teleport { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveShowCloseButton = ShowCloseButton ?? true;

        if (!string.IsNullOrWhiteSpace(Teleport))
        {
            output.TagName = "template";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("x-teleport", Teleport);
        }
        else
        {
            output.TagName = string.Empty;
        }

        /*
         * Render overlay
         */
        var overlayTagBuilder = new TagBuilder("div");
        overlayTagBuilder.Attributes.Add("x-bind", "overlay");
        overlayTagBuilder.Attributes.Add("data-slot", "dialog-overlay");
        overlayTagBuilder.Attributes.Add(
            "class",
            ClassMerger.Merge(new ThemeToken("dui-dialog-overlay"), "fixed inset-0 isolate z-50")
        );

        /*
         * Render content
         */
        var dialogContentTagBuilder = new TagBuilder("div");
        dialogContentTagBuilder.Attributes.Add("x-bind", "dialog");
        dialogContentTagBuilder.Attributes.Add("role", "dialog");
        dialogContentTagBuilder.Attributes.Add("data-slot", "dialog-content");
        dialogContentTagBuilder.Attributes.Add(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-dialog-content"),
                "fixed top-1/2 left-1/2 z-50 w-full -translate-x-1/2 -translate-y-1/2 outline-none",
                output.GetUserSuppliedClass()
            )
        );
        if (effectiveShowCloseButton)
        {
            var iconOutput = new TagHelperOutput(
                "svg",
                [],
                (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
            );
            var iconTagHelper = new IconTagHelper(ThemeManager, ClassMerger, _iconManager)
            {
                Name = "x",
            };
            await iconTagHelper.ProcessAsync(context, iconOutput);

            var closeButtonTagHelperContent = new DefaultTagHelperContent();
            closeButtonTagHelperContent.SetHtmlContent(iconOutput);

            var closeButtonOutput = new TagHelperOutput(
                "button",
                [
                    new TagHelperAttribute(
                        "class",
                        ClassMerger.Merge(new ThemeToken("dui-dialog-close"))
                    ),
                    new TagHelperAttribute("x-bind", "closeButton"),
                ],
                (_, _) => Task.FromResult<TagHelperContent>(closeButtonTagHelperContent)
            );
            var buttonTagHelper = new ButtonTagHelper(ThemeManager, ClassMerger)
            {
                Size = ButtonSize.IconSmall,
                Variant = ButtonVariant.Ghost,
            };
            await buttonTagHelper.ProcessAsync(context, closeButtonOutput);
            dialogContentTagBuilder.InnerHtml.AppendHtml(closeButtonOutput);
        }

        dialogContentTagBuilder.InnerHtml.AppendHtml(await output.GetChildContentAsync());
        overlayTagBuilder.InnerHtml.AppendHtml(dialogContentTagBuilder);

        output.Content.AppendHtml(overlayTagBuilder);
    }
}
