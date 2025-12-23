using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-sidebar")]
public class SidebarTagHelper : DuneUITagHelperBase
{
    public SidebarTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    [HtmlAttributeName("variant")]
    public SidebarVariant Variant { get; set; } = SidebarVariant.Sidebar;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var state = "expanded";
        var collapsible = "offcanvas";
        var side = "left";
        var variant = GetVariantName(Variant);

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar");
        output.Attributes.SetAttribute("data-state", state);
        output.Attributes.SetAttribute("data-collapsible", state == "collapsed" ? collapsible : "");
        output.Attributes.SetAttribute("data-variant", variant);
        output.Attributes.SetAttribute("data-side", side);

        output.Attributes.SetAttribute(
            "class",
            "group peer text-sidebar-foreground hidden md:block"
        );

        /* Gap */
        var gapTagBuilder = new TagBuilder("div");
        gapTagBuilder.Attributes.Add("data-slot", "sidebar-gap");
        gapTagBuilder.Attributes.Add(
            "class",
            ClassMerger.Merge(
                "relative w-(--sidebar-width) bg-transparent transition-[width] duration-200 ease-linear",
                "group-data-[collapsible=offcanvas]:w-0",
                "group-data-[side=right]:rotate-180",
                variant is "floating" or "inset"
                    ? "group-data-[collapsible=icon]:w-[calc(var(--sidebar-width-icon)+(--spacing(4)))]"
                    : "group-data-[collapsible=icon]:w-(--sidebar-width-icon)"
            )
        );
        output.Content.AppendHtml(gapTagBuilder);

        /* Sidebar container */
        var sidebarContainerTagBuilder = new TagBuilder("div");
        sidebarContainerTagBuilder.Attributes.Add("data-slot", "sidebar-container");
        sidebarContainerTagBuilder.Attributes.Add(
            "class",
            ClassMerger.Merge(
                "fixed inset-y-0 z-10 hidden h-svh w-(--sidebar-width) transition-[left,right,width] duration-200 ease-linear md:flex",
                side == "left"
                    ? "left-0 group-data-[collapsible=offcanvas]:left-[calc(var(--sidebar-width)*-1)]"
                    : "right-0 group-data-[collapsible=offcanvas]:right-[calc(var(--sidebar-width)*-1)]",
                // Adjust the padding for floating and inset variants.
                variant is "floating" or "inset"
                    ? "p-2 group-data-[collapsible=icon]:w-[calc(var(--sidebar-width-icon)+(--spacing(4))+2px)]"
                    : "group-data-[collapsible=icon]:w-(--sidebar-width-icon) group-data-[side=left]:border-r group-data-[side=right]:border-l",
                output.GetUserSuppliedClass()
            )
        );

        /* Sidebar inner */
        var sidebarInnerTagBuilder = new TagBuilder("div");
        sidebarInnerTagBuilder.Attributes.Add("data-sidebar", "sidebar");
        sidebarInnerTagBuilder.Attributes.Add("data-slot", "sidebar-inner");
        sidebarInnerTagBuilder.Attributes.Add(
            "class",
            "bg-sidebar group-data-[variant=floating]:border-sidebar-border flex h-full w-full flex-col group-data-[variant=floating]:rounded-lg group-data-[variant=floating]:border group-data-[variant=floating]:shadow-sm"
        );
        sidebarInnerTagBuilder.InnerHtml.AppendHtml(await output.GetChildContentAsync());

        sidebarContainerTagBuilder.InnerHtml.AppendHtml(sidebarInnerTagBuilder);

        output.Content.AppendHtml(sidebarContainerTagBuilder);
    }

    private string GetVariantName(SidebarVariant variant)
    {
        return variant switch
        {
            SidebarVariant.Floating => "floating",
            SidebarVariant.Inset => "inset",
            _ => "sidebar",
        };
    }
}
