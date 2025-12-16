using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-sidebar-menu")]
public class SidebarMenuTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public SidebarMenuTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "ul";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-menu");
        output.Attributes.SetAttribute("data-sidebar", "menu");
        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge("flex w-full min-w-0 flex-col gap-1", output.GetUserSuppliedClass())
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
