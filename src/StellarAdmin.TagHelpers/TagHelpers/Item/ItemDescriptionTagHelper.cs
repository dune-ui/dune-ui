using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-item-description")]
public class ItemDescriptionTagHelper : StellarTagHelper
{
    public ItemDescriptionTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "p";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "item-description");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-item-description"),
                "[&>a:hover]:text-primary line-clamp-2 font-normal [&>a]:underline [&>a]:underline-offset-4",
                GetUserSpecifiedClass(output)
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
