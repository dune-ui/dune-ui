using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-sidebar-menu-link", Attributes = Href)]
[HtmlTargetElement("sui-sidebar-menu-link", Attributes = ActionAttributeName)]
[HtmlTargetElement("sui-sidebar-menu-link", Attributes = ControllerAttributeName)]
[HtmlTargetElement("sui-sidebar-menu-link", Attributes = AreaAttributeName)]
[HtmlTargetElement("sui-sidebar-menu-link", Attributes = PageAttributeName)]
[HtmlTargetElement("sui-sidebar-menu-link", Attributes = PageHandlerAttributeName)]
[HtmlTargetElement("sui-sidebar-menu-link", Attributes = FragmentAttributeName)]
[HtmlTargetElement("sui-sidebar-menu-link", Attributes = HostAttributeName)]
[HtmlTargetElement("sui-sidebar-menu-link", Attributes = ProtocolAttributeName)]
[HtmlTargetElement("sui-sidebar-menu-link", Attributes = RouteAttributeName)]
[HtmlTargetElement("sui-sidebar-menu-link", Attributes = RouteValuesDictionaryName)]
[HtmlTargetElement("sui-sidebar-menu-link", Attributes = RouteValuesPrefix + "*")]
public class SidebarMenuLinkAnchorTagHelper(IHtmlGenerator generator) : AnchorTagHelper(generator)
{
    private const string ActionAttributeName = "asp-action";
    private const string ControllerAttributeName = "asp-controller";
    private const string AreaAttributeName = "asp-area";
    private const string PageAttributeName = "asp-page";
    private const string PageHandlerAttributeName = "asp-page-handler";
    private const string FragmentAttributeName = "asp-fragment";
    private const string HostAttributeName = "asp-host";
    private const string ProtocolAttributeName = "asp-protocol";
    private const string RouteAttributeName = "asp-route";
    private const string RouteValuesDictionaryName = "asp-all-route-data";
    private const string RouteValuesPrefix = "asp-route-";
    private const string Href = "href";

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        await base.ProcessAsync(context, output);

        var isActiveRoute = false;

        var isRouteLink = Route != null;
        var isActionLink = Controller != null || Action != null;
        var isPageLink = Page != null || PageHandler != null;

        if (isRouteLink)
        {
            // TODO: ...
        }
        else if (isActionLink)
        {
            if (
                ViewContext.RouteData.Values["area"]?.ToString() == Area
                && ViewContext.RouteData.Values["controller"]?.ToString() == Controller
                && ViewContext.RouteData.Values["action"]?.ToString() == Action
            )
            {
                isActiveRoute = true;
            }
        }
        else if (isPageLink)
        {
            if (
                ViewContext.RouteData.Values["area"]?.ToString() == Area
                && ViewContext.RouteData.Values["page"]?.ToString() == Page
                && ViewContext.RouteData.Values["handler"]?.ToString() == PageHandler
            )
            {
                isActiveRoute = true;
            }
        }

        output.Attributes.SetAttribute("data-active", isActiveRoute ? "true" : "false");
    }
}
