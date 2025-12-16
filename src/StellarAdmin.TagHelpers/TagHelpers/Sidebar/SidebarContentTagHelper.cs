using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-sidebar-content")]
public class SidebarContentTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public SidebarContentTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-content");
        output.Attributes.SetAttribute("data-sidebar", "content");
        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge(
                "flex min-h-0 flex-1 flex-col gap-2 overflow-auto group-data-[collapsible=icon]:overflow-hidden",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
