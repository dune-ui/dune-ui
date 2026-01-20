using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-input-group-text")]
public class InputGroupTextTagHelper : DuneUITagHelperBase
{
    public InputGroupTextTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-input-group-text"),
                "flex items-center [&_svg]:pointer-events-none",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
