using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-card")]
public class CardTagHelper : StellarTagHelper
{
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
                new ComponentName("dui-card"),
                "group/card flex flex-col",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
