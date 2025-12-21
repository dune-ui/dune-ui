using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-item-actions")]
public class ItemActionsTagHelper : StellarTagHelper
{
    public ItemActionsTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "item-actions");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-item-actions"),
                "flex items-center",
                GetUserSpecifiedClass(output)
            )
        );

        return Task.CompletedTask;
    }
}
