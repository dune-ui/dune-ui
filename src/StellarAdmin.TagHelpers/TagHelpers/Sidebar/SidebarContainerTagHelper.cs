using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-sidebar-wrapper")]
public class SidebarContainerTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public SidebarContainerTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    private const string SidebarWidth = "16rem";
    private const string SidebarWidthIcon = "3rem";

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar-wrapper");
        output.Attributes.SetAttribute(
            "style",
            $"--sidebar-width: {SidebarWidth}; --sidebar-width-icon: {SidebarWidthIcon}"
        );
        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge(
                "group/sidebar-wrapper has-data-[variant=inset]:bg-sidebar flex min-h-svh w-full",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
