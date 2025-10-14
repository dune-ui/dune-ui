using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-sidebar-menu-link")]
public class SidebarMenuLinkTagHelper(IHtmlGenerator generator, ICssClassMerger classMerger)
    : StellarAnchorTagHelperBase(generator)
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var size = "default";
        var variant = "default";

        output.TagName = "a";
        output.TagMode = TagMode.StartTagAndEndTag;

        var isActiveRoute = ApplyAnchorAttributes(output);

        output.Attributes.SetAttribute("data-active", isActiveRoute ? "true" : "false");
        output.Attributes.SetAttribute("data-slot", "sidebar-menu-button");
        output.Attributes.SetAttribute("data-sidebar", "menu-button");
        output.Attributes.SetAttribute("data-size", size);
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "peer/menu-button flex w-full items-center gap-2 overflow-hidden rounded-md p-2 text-left text-sm outline-hidden ring-sidebar-ring transition-[width,height,padding] hover:bg-sidebar-accent hover:text-sidebar-accent-foreground focus-visible:ring-2 active:bg-sidebar-accent active:text-sidebar-accent-foreground disabled:pointer-events-none disabled:opacity-50 group-has-data-[sidebar=menu-action]/menu-item:pr-8 aria-disabled:pointer-events-none aria-disabled:opacity-50 data-[active=true]:bg-sidebar-accent data-[active=true]:font-medium data-[active=true]:text-sidebar-accent-foreground data-[state=open]:hover:bg-sidebar-accent data-[state=open]:hover:text-sidebar-accent-foreground group-data-[collapsible=icon]:size-8! group-data-[collapsible=icon]:p-2! [&>span:last-child]:truncate [&>svg]:size-4 [&>svg]:shrink-0",
                "hover:bg-sidebar-accent hover:text-sidebar-accent-foreground", // default variant
                "h-8 text-sm", // default size
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
