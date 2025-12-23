using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-empty-title")]
public class EmptyTitleTagHelper : DuneUITagHelperBase
{
    public EmptyTitleTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "empty-title");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge("dui-empty-title", output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
