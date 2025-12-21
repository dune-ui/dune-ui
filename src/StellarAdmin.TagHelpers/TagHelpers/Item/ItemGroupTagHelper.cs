using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-item-group")]
public class ItemGroupTagHelper : StellarTagHelper
{
    public ItemGroupTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("role", "list");
        output.Attributes.SetAttribute("data-slot", "item-group");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-item-group"),
                "group/item-group flex w-full flex-col",
                GetUserSpecifiedClass(output)
            )
        );

        return Task.CompletedTask;
    }
}
