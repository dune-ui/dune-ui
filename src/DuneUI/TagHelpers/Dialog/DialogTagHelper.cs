using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-dialog")]
public class DialogTagHelper : DuneUITagHelperBase
{
    public DialogTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    [HtmlAttributeName("is-dismissable")]
    public bool? IsDismissable { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveIsDismissable = IsDismissable ?? true;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "x-data",
            AlpineJsDataSerializer.SerializeDataFunction("dialog", [false, effectiveIsDismissable])
        );
        output.Attributes.SetAttribute("x-bind", "root");

        output.Attributes.SetAttribute("data-slot", "dialog");

        return Task.CompletedTask;
    }
}

[HtmlTargetElement("dui-dialog-trigger")]
public class DialogTriggerTagHelper : DuneUITagHelperBase
{
    public DialogTriggerTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("x-bind", "trigger");

        output.Attributes.SetAttribute("data-slot", "dialog-trigger");

        return Task.CompletedTask;
    }
}

[HtmlTargetElement("dui-dialog-content")]
public class DialogContentTagHelper : DuneUITagHelperBase
{
    [HtmlAttributeName("teleport")]
    public string? Teleport { get; set; }

    public DialogContentTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
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

        var overlayTagBuilder = new TagBuilder("div");
        overlayTagBuilder.Attributes.Add("x-bind", "overlay");
        overlayTagBuilder.Attributes.Add("data-slot", "dialog-overlay");
        overlayTagBuilder.Attributes.Add("class", "fixed min-h-[100vh] inset-0 z-50 bg-black/50");

        var dialogTagBuilder = new TagBuilder("div");
        dialogTagBuilder.Attributes.Add("x-bind", "dialog");
        dialogTagBuilder.Attributes.Add("role", "dialog");
        dialogTagBuilder.Attributes.Add("data-slot", "dialog-content");
        dialogTagBuilder.Attributes.Add(
            "class",
            BuildClassString(
                null,
                [
                    "fixed left-[50%] top-[50%] z-50 grid w-full max-w-lg translate-x-[-50%] translate-y-[-50%] gap-4 border bg-background p-6 shadow-lg duration-200 sm:rounded-lg",
                    output.GetUserSuppliedClass(),
                ]
            )
        );
        dialogTagBuilder.InnerHtml.AppendHtml(await output.GetChildContentAsync());
        overlayTagBuilder.InnerHtml.AppendHtml(dialogTagBuilder);

        output.Content.AppendHtml(overlayTagBuilder);
    }
}
