using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     The main content region of an item; typically wraps the title and description.
/// </summary>
[HtmlTargetElement("dui-item-content")]
public class ItemContentTagHelper : DuneUITagHelperBase
{
    public ItemContentTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "item-content");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-item-content"),
                "flex flex-1 flex-col [&+[data-slot=item-content]]:flex-none",
                GetUserSpecifiedClass(output)
            )
        );

        return Task.CompletedTask;
    }
}
