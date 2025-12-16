using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-sidebar-menu-item")]
public class SidebarMenuItemTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public SidebarMenuItemTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "li";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-menu-item");
        output.Attributes.SetAttribute("data-sidebar", "menu-item");
        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge("group/menu-item relative", output.GetUserSuppliedClass())
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
