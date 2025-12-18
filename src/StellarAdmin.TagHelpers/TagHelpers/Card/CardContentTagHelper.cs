using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-card-content")]
public class CardContentTagHelper : StellarTagHelper
{
    public CardContentTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "card-content");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(new ComponentName("dui-card-content"), output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
