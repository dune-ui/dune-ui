using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-dialog")]
public class DialogTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "dialog";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "dialog-content");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-dialog-content"),
                "fixed inset-0 m-auto outline-none",
                "backdrop:supports-backdrop-filter:backdrop-blur-xs",
                output.GetUserSuppliedClass()
            )
        );

        // Wrap inside web component
        output.PreElement.AppendHtml("<del-dialog>");
        output.PostElement.AppendHtml("</del-dialog>");

        return Task.CompletedTask;
    }
}
