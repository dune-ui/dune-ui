using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-item-separator")]
public class ItemSeparatorTagHelper : StellarTagHelper
{
    public ItemSeparatorTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.SetAttribute("data-slot", "item-separator");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-item-separator"),
                GetUserSpecifiedClass(output)
            )
        );

        var separatorTagHelper = new SeparatorTagHelper(ThemeManager, ClassMerger)
        {
            Orientation = SeparatorOrientation.Horizontal,
        };
        await separatorTagHelper.ProcessAsync(context, output);
    }
}
