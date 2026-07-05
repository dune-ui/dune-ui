using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     The header region of an item, spanning its full width above the main content.
/// </summary>
[HtmlTargetElement("dui-item-header")]
public class ItemHeaderTagHelper : DuneUITagHelperBase
{
    public ItemHeaderTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "item-header");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-item-header"),
                "flex basis-full items-center justify-between",
                GetUserSpecifiedClass(output)
            )
        );

        return Task.CompletedTask;
    }
}
