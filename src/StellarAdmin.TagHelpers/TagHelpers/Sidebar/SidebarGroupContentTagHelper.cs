using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-sidebar-group-content")]
public class SidebarGroupContentTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public SidebarGroupContentTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-group-content");
        output.Attributes.SetAttribute("data-sidebar", "group-content");
        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge("w-full text-sm", output.GetUserSuppliedClass())
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
