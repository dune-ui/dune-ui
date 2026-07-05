using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A flexible container that groups related content, composed of a header, title,
///     description, content, footer, and action subcomponents.
/// </summary>
[HtmlTargetElement("dui-card")]
public class CardTagHelper : DuneUITagHelperBase
{
    /// <summary>
    ///     The size of the card, which controls its padding and spacing.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="CardSize.Default" />.
    /// </remarks>
    [HtmlAttributeName("size")]
    public CardSize? Size { get; set; }

    public CardTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveSize = Size ?? CardSize.Default;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "card");
        output.Attributes.SetAttribute("data-size", effectiveSize.GetDataAttributeText());
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ThemeToken("dui-card"),
                "group/card flex flex-col",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
