using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-empty")]
public class EmptyTagHelper : DuneUITagHelperBase
{
    public EmptyTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "empty");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-empty"),
                "flex w-full min-w-0 flex-1 flex-col items-center justify-center text-center text-balance",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
