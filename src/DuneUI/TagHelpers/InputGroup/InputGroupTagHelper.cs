using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-input-group")]
public class InputGroupTagHelper : DuneUITagHelperBase
{
    public InputGroupTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("role", "group");
        output.Attributes.SetAttribute("data-slot", "input-group");

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-input-group"),
                "group/input-group relative flex w-full min-w-0 items-center outline-none has-[>textarea]:h-auto",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
