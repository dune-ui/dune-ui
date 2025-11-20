using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-sidebar-wrapper")]
public class SidebarContainerTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
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
            classMerger.Merge(
                "group/sidebar-wrapper has-data-[variant=inset]:bg-sidebar flex min-h-svh w-full",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
