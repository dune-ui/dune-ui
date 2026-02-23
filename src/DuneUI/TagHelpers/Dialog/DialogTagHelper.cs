using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-dialog")]
public class DialogTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
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
