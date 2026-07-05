using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     The footer region of a card; typically contains actions or supplementary information.
/// </summary>
[HtmlTargetElement("dui-card-footer")]
public class CardFooterTagHelper : DuneUITagHelperBase
{
    public CardFooterTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "card-footer");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ThemeToken("dui-card-footer"),
                "flex items-center",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
