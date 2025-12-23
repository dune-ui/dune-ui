using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-card-description")]
public class CardDescriptionTagHelper : DuneUITagHelperBase
{
    public CardDescriptionTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "card-description");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ComponentName("dui-card-description"),
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
