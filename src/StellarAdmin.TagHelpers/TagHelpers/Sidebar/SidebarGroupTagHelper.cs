using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-sidebar-group")]
public class SidebarGroupTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public SidebarGroupTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-group");
        output.Attributes.SetAttribute("data-sidebar", "group");
        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge(
                "relative flex w-full min-w-0 flex-col p-2",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
