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
    public SidebarVariant? Variant { get; set; }

    [HtmlAttributeName("side")]
    public SidebarSide? Side { get; set; }

    [HtmlAttributeName("collapsible")]
    public SidebarCollapsible? Collapsible { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveVariant = Variant ?? SidebarVariant.Sidebar;
        var effectiveSide = Side ?? SidebarSide.Left;
        var effectiveCollapsible = Collapsible ?? SidebarCollapsible.Offcanvas;

        var isFloatingOrInset = effectiveVariant is SidebarVariant.Floating or SidebarVariant.Inset;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sidebar");
        output.Attributes.SetAttribute("data-variant", effectiveVariant.GetDataAttributeText());
        output.Attributes.SetAttribute("data-side", effectiveSide.GetDataAttributeText());

        // collapsible="none": a static, always-visible sidebar with no toggle and
        // no mobile drawer (mirrors shadcn's `collapsible="none"` branch).
        if (effectiveCollapsible == SidebarCollapsible.None)
        {
            output.Attributes.SetAttribute(
                "data-collapsible",
                effectiveCollapsible.GetDataAttributeText()
            );
            output.Attributes.SetAttribute(
                "class",
                ClassMerger.Merge(
                    new ThemeToken("dui-sidebar-inner"),
                    "text-sidebar-foreground flex h-svh w-(--sidebar-width) flex-col",
                    effectiveSide == SidebarSide.Left ? "border-r" : "border-l",
                    output.GetUserSuppliedClass()
                )
            );

            output.Content.AppendHtml(await output.GetChildContentAsync());
            return;
        }

        // Initial state — the `del-sidebar` provider keeps these in sync at runtime.
        output.Attributes.SetAttribute("data-state", "expanded");
        output.Attributes.SetAttribute("data-mobile", "closed");
        // data-collapsible is dynamic (set to the mode only while collapsed on desktop);
        // the provider reads the static mode from data-collapsible-config.
        output.Attributes.SetAttribute("data-collapsible", "");
        output.Attributes.SetAttribute(
            "data-collapsible-config",
            effectiveCollapsible.GetDataAttributeText()
        );
        output.Attributes.SetAttribute(
            "class",
            "group peer text-sidebar-foreground data-[side=right]:order-last"
        );

        /* Backdrop — mobile only. Fades in behind the drawer and closes it on click.
           A <button> so the native command API fires; targets the parent del-sidebar. */
        var sidebarId = GetParentTagHelper<SidebarWrapperTagHelper>()?.SidebarId;
        var backdropTagBuilder = new TagBuilder("button");
        backdropTagBuilder.Attributes.Add("type", "button");
        backdropTagBuilder.Attributes.Add("data-slot", "sidebar-backdrop");
        backdropTagBuilder.Attributes.Add("aria-label", "Close sidebar");
        if (sidebarId != null)
        {
            backdropTagBuilder.Attributes.Add("command", "--close-mobile");
            backdropTagBuilder.Attributes.Add("commandfor", sidebarId);
        }
        backdropTagBuilder.Attributes.Add(
            "class",
            "fixed inset-0 z-20 bg-black/50 opacity-0 transition-opacity duration-200 ease-linear "
                + "pointer-events-none md:hidden "
                + "group-data-[mobile=open]:opacity-100 group-data-[mobile=open]:pointer-events-auto"
        );
        output.Content.AppendHtml(backdropTagBuilder);

        /* Gap — desktop-only spacer that pushes the inset content. */
        var gapTagBuilder = new TagBuilder("div");
        gapTagBuilder.Attributes.Add("data-slot", "sidebar-gap");
        gapTagBuilder.Attributes.Add(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-sidebar-gap"),
                "relative hidden h-svh w-(--sidebar-width) bg-transparent md:block",
                "group-data-[collapsible=offcanvas]:w-0",
                isFloatingOrInset
                    ? "group-data-[collapsible=icon]:w-[calc(var(--sidebar-width-icon)+(--spacing(4)))]"
                    : "group-data-[collapsible=icon]:w-(--sidebar-width-icon)"
            )
        );
        output.Content.AppendHtml(gapTagBuilder);

        /* Sidebar container — desktop panel + mobile drawer in one element.
           Desktop  : anchored by left/right; collapses via left/right offset (offcanvas)
                      or width (icon). data-collapsible is only set on desktop.
           Mobile   : off-canvas drawer driven by translateX (max-md only), so the
                      desktop left/right rules never apply at mobile widths. */
        var sidebarContainerTagBuilder = new TagBuilder("div");
        sidebarContainerTagBuilder.Attributes.Add("data-slot", "sidebar-container");
        sidebarContainerTagBuilder.Attributes.Add(
            "class",
            ClassMerger.Merge(
                "fixed inset-y-0 z-30 flex h-svh w-(--sidebar-width-mobile) transition-[left,right,width,transform] duration-200 ease-linear md:w-(--sidebar-width)",
                // Anchor edge per side.
                "group-data-[side=left]:left-0 group-data-[side=right]:right-0",
                // Mobile drawer (max-md): slide toward the anchored edge when closed.
                "max-md:group-data-[mobile=closed]:group-data-[side=left]:-translate-x-full",
                "max-md:group-data-[mobile=closed]:group-data-[side=right]:translate-x-full",
                "max-md:group-data-[mobile=open]:translate-x-0",
                // Desktop offcanvas: slide off-screen toward the anchored edge.
                "group-data-[collapsible=offcanvas]:group-data-[side=left]:left-[calc(var(--sidebar-width)*-1)]",
                "group-data-[collapsible=offcanvas]:group-data-[side=right]:right-[calc(var(--sidebar-width)*-1)]",
                // Desktop icon-rail width + padding/border per variant.
                isFloatingOrInset
                    ? "p-2 group-data-[collapsible=icon]:w-[calc(var(--sidebar-width-icon)+(--spacing(4))+2px)]"
                    : "group-data-[side=left]:border-r group-data-[side=right]:border-l group-data-[collapsible=icon]:w-(--sidebar-width-icon)",
                output.GetUserSuppliedClass()
            )
        );

        /* Sidebar inner */
        var sidebarInnerTagBuilder = new TagBuilder("div");
        sidebarInnerTagBuilder.Attributes.Add("data-sidebar", "sidebar");
        sidebarInnerTagBuilder.Attributes.Add("data-slot", "sidebar-inner");
        sidebarInnerTagBuilder.Attributes.Add(
            "class",
            ClassMerger.Merge(new ThemeToken("dui-sidebar-inner"), "flex size-full flex-col")
        );
        sidebarInnerTagBuilder.InnerHtml.AppendHtml(await output.GetChildContentAsync());

        sidebarContainerTagBuilder.InnerHtml.AppendHtml(sidebarInnerTagBuilder);

        output.Content.AppendHtml(sidebarContainerTagBuilder);
    }
}
