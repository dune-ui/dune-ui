using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-sidebar-separator")]
public class SidebarSeparatorTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.SetAttribute("data-slot", "sidebar-separator");
        output.Attributes.SetAttribute("data-sidebar", "separator");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-sidebar-separator"),
                "w-auto",
                output.GetUserSuppliedClass()
            )
        );

        var separatorTagHelper = new SeparatorTagHelper(ThemeManager, ClassMerger)
        {
            Orientation = SeparatorOrientation.Horizontal,
        };

        await separatorTagHelper.ProcessAsync(context, output);
    }
}