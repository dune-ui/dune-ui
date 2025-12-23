using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-card-header")]
public class CardHeaderTagHelper : DuneUITagHelperBase
{
    public CardHeaderTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "card-header");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ComponentName("dui-card-header"),
                "group/card-header @container/card-header grid auto-rows-min items-start has-data-[slot=card-action]:grid-cols-[1fr_auto] has-data-[slot=card-description]:grid-rows-[auto_auto]",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
