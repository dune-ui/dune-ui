using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-dialog-footer")]
public class DialogFooterTagHelper : DuneUITagHelperBase
{
    public bool? ShowCloseButton { get; set; }

    public DialogFooterTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveShowCloseButton = ShowCloseButton ?? false;

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

        if (effectiveShowCloseButton)
        {
            var closeButtonContent = new DefaultTagHelperContent();
            closeButtonContent.Append("Close");

            var closeButtonOutput = new TagHelperOutput(
                "",
                [new TagHelperAttribute("x-bind", "closeButton")],
                (_, _) => Task.FromResult<TagHelperContent>(closeButtonContent)
            );
            var buttonTagHelper = new ButtonTagHelper(ThemeManager, ClassMerger)
            {
                Variant = ButtonVariant.Outline,
            };
            await buttonTagHelper.ProcessAsync(context, closeButtonOutput);

            output.Content.AppendHtml(closeButtonOutput);
        }

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
