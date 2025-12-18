using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-card-action")]
public class CardActionTagHelper : StellarTagHelper
{
    public CardActionTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "card-action");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ComponentName("dui-card-action"),
                "col-start-2 row-span-2 row-start-1 self-start justify-self-end",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
