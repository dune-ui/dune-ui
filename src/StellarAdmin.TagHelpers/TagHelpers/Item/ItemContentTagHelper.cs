using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-item-content")]
public class ItemContentTagHelper : StellarTagHelper
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
                new ComponentName("dui-item-content"),
                "flex flex-1 flex-col [&+[data-slot=item-content]]:flex-none",
                GetUserSpecifiedClass(output)
            )
        );

        return Task.CompletedTask;
    }
}
