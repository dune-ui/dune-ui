using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using FrameworkAnchorTagHelper = Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-sidebar-menu-link")]
public class SidebarMenuLinkTagHelper : StellarAnchorTagHelperBase
{
    private readonly IHtmlGenerator _htmlGenerator;
    private readonly ICssClassMerger _classMerger;

    public SidebarMenuLinkTagHelper(IHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
    {
        _htmlGenerator = htmlGenerator ?? throw new ArgumentNullException(nameof(htmlGenerator));
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var size = "default";
        var variant = "default";

        output.TagName = "a";
        output.TagMode = TagMode.StartTagAndEndTag;

        var anchorTagHelper = new FrameworkAnchorTagHelper(_htmlGenerator)
        {
            ViewContext = ViewContext,
            Action = Action,
            Area = Area,
            Controller = Controller,
            Fragment = Fragment,
            Host = Host,
            Page = Page,
            PageHandler = PageHandler,
            Protocol = Protocol,
            Route = Route,
            RouteValues = RouteValues,
        };
        await anchorTagHelper.ProcessAsync(context, output);

        output.Attributes.SetAttribute("data-active", IsActiveRoute() ? "true" : "false");
        output.Attributes.SetAttribute("data-slot", "sidebar-menu-button");
        output.Attributes.SetAttribute("data-sidebar", "menu-button");
        output.Attributes.SetAttribute("data-size", size);
        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge(
                "peer/menu-button flex w-full items-center gap-2 overflow-hidden rounded-md p-2 text-left text-sm outline-hidden ring-sidebar-ring transition-[width,height,padding] hover:bg-sidebar-accent hover:text-sidebar-accent-foreground focus-visible:ring-2 active:bg-sidebar-accent active:text-sidebar-accent-foreground disabled:pointer-events-none disabled:opacity-50 group-has-data-[sidebar=menu-action]/menu-item:pr-8 aria-disabled:pointer-events-none aria-disabled:opacity-50 data-[active=true]:bg-sidebar-accent data-[active=true]:font-medium data-[active=true]:text-sidebar-accent-foreground data-[state=open]:hover:bg-sidebar-accent data-[state=open]:hover:text-sidebar-accent-foreground group-data-[collapsible=icon]:size-8! group-data-[collapsible=icon]:p-2! [&>span:last-child]:truncate [&>svg]:size-4 [&>svg]:shrink-0",
                "hover:bg-sidebar-accent hover:text-sidebar-accent-foreground", // default variant
                "h-8 text-sm", // default size
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }

    private bool IsActiveRoute()
    {
        var isRouteLink = Route != null;
        var isActionLink = Controller != null || Action != null;
        var isPageLink = Page != null || PageHandler != null;

        if (isPageLink)
        {
            return ViewContext.RouteData.Values["area"]?.ToString() == Area
                && ViewContext.RouteData.Values["page"]?.ToString() == Page
                && ViewContext.RouteData.Values["handler"]?.ToString() == PageHandler;
        }
        else if (isActionLink)
        {
            return ViewContext.RouteData.Values["area"]?.ToString() == Area
                && ViewContext.RouteData.Values["controller"]?.ToString() == Controller
                && ViewContext.RouteData.Values["action"]?.ToString() == Action;
        }

        return false;
    }
}
