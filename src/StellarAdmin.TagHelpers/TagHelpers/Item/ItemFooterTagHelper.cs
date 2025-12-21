using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-item-footer")]
public class ItemFooterTagHelper : StellarTagHelper
{
    public ItemFooterTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "item-footer");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-item-footer"),
                "flex basis-full items-center justify-between",
                GetUserSpecifiedClass(output)
            )
        );

        return Task.CompletedTask;
    }
}
