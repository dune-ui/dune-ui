using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-card-title")]
public class CardTitleTagHelper : StellarTagHelper
{
    public CardTitleTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "card-title");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(new ComponentName("dui-card-title"), output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
