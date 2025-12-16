using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-sidebar-header")]
public class SidebarHeaderTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public SidebarHeaderTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-header");
        output.Attributes.SetAttribute("data-sidebar", "header");
        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge("flex flex-col gap-2 p-2", output.GetUserSuppliedClass())
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
