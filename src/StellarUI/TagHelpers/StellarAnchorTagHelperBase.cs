using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace StellarUI.TagHelpers;

public class StellarAnchorTagHelperBase : StellarTagHelper
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
    private IDictionary<string, string?>? _routeValues;

    public StellarAnchorTagHelperBase(IHtmlGenerator htmlGenerator)
    {
        Generator = htmlGenerator;
    }

    /// <summary>
    /// Gets the <see cref="IHtmlGenerator"/> used to generate the <see cref="AnchorTagHelper"/>'s output.
    /// </summary>
    protected IHtmlGenerator Generator { get; }

    /// <summary>
    /// The name of the action method.
    /// </summary>
    /// <remarks>
    /// Must be <c>null</c> if <see cref="Route"/> or <see cref="Page"/> is non-<c>null</c>.
    /// </remarks>
    [HtmlAttributeName(ActionAttributeName)]
    public string? Action { get; set; }

    /// <summary>
    /// The name of the controller.
    /// </summary>
    /// <remarks>
    /// Must be <c>null</c> if <see cref="Route"/> or <see cref="Page"/> is non-<c>null</c>.
    /// </remarks>
    [HtmlAttributeName(ControllerAttributeName)]
    public string? Controller { get; set; }

    /// <summary>
    /// The name of the area.
    /// </summary>
    /// <remarks>
    /// Must be <c>null</c> if <see cref="Route"/> is non-<c>null</c>.
    /// </remarks>
    [HtmlAttributeName(AreaAttributeName)]
    public string? Area { get; set; }

    /// <summary>
    /// The name of the page.
    /// </summary>
    /// <remarks>
    /// Must be <c>null</c> if <see cref="Route"/> or <see cref="Action"/>, <see cref="Controller"/>
    /// is non-<c>null</c>.
    /// </remarks>
    [HtmlAttributeName(PageAttributeName)]
    public string? Page { get; set; }

    /// <summary>
    /// The name of the page handler.
    /// </summary>
    /// <remarks>
    /// Must be <c>null</c> if <see cref="Route"/> or <see cref="Action"/>, or <see cref="Controller"/>
    /// is non-<c>null</c>.
    /// </remarks>
    [HtmlAttributeName(PageHandlerAttributeName)]
    public string? PageHandler { get; set; }

    /// <summary>
    /// The protocol for the URL, such as &quot;http&quot; or &quot;https&quot;.
    /// </summary>
    [HtmlAttributeName(ProtocolAttributeName)]
    public string? Protocol { get; set; }

    /// <summary>
    /// The host name.
    /// </summary>
    [HtmlAttributeName(HostAttributeName)]
    public string? Host { get; set; }

    /// <summary>
    /// The URL fragment name.
    /// </summary>
    [HtmlAttributeName(FragmentAttributeName)]
    public string? Fragment { get; set; }

    /// <summary>
    /// Name of the route.
    /// </summary>
    /// <remarks>
    /// Must be <c>null</c> if one of <see cref="Action"/>, <see cref="Controller"/>, <see cref="Area"/>
    /// or <see cref="Page"/> is non-<c>null</c>.
    /// </remarks>
    [HtmlAttributeName(RouteAttributeName)]
    public string? Route { get; set; }

    /// <summary>
    /// Additional parameters for the route.
    /// </summary>
    [HtmlAttributeName(RouteValuesDictionaryName, DictionaryAttributePrefix = RouteValuesPrefix)]
    public IDictionary<string, string?> RouteValues
    {
        get
        {
            _routeValues ??= new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

            return _routeValues;
        }
        set { _routeValues = value; }
    }

    /// <summary>
    /// Gets or sets the <see cref="Microsoft.AspNetCore.Mvc.Rendering.ViewContext"/> for the current request.
    /// </summary>
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }

    protected bool ApplyAnchorAttributes(TagHelperOutput output)
    {
        if (output.Attributes.ContainsName(Href))
        {
            return false;
        }

        var routeLink = Route != null;
        var actionLink = Controller != null || Action != null;
        var pageLink = Page != null || PageHandler != null;

        if ((routeLink && actionLink) || (routeLink && pageLink) || (actionLink && pageLink))
        {
            throw new InvalidOperationException(
                "You cannot mix route, action, and page attributes"
            );
        }

        var isActiveRoute = false;
        RouteValueDictionary? routeValues = null;
        if (_routeValues is { Count: > 0 })
        {
            routeValues = new RouteValueDictionary(_routeValues);
        }

        if (Area != null)
        {
            if (routeValues == null)
            {
                routeValues = new RouteValueDictionary();
            }
            routeValues["area"] = Area;
        }

        TagBuilder tagBuilder;
        if (pageLink)
        {
            tagBuilder = Generator.GeneratePageLink(
                ViewContext,
                linkText: string.Empty,
                pageName: Page,
                pageHandler: PageHandler,
                protocol: Protocol,
                hostname: Host,
                fragment: Fragment,
                routeValues: routeValues,
                htmlAttributes: null
            );

            isActiveRoute =
                ViewContext.RouteData.Values["area"]?.ToString() == Area
                && ViewContext.RouteData.Values["page"]?.ToString() == Page
                && ViewContext.RouteData.Values["handler"]?.ToString() == PageHandler;
        }
        else if (routeLink)
        {
            tagBuilder = Generator.GenerateRouteLink(
                ViewContext,
                linkText: string.Empty,
                routeName: Route,
                protocol: Protocol,
                hostName: Host,
                fragment: Fragment,
                routeValues: routeValues,
                htmlAttributes: null
            );
        }
        else
        {
            tagBuilder = Generator.GenerateActionLink(
                ViewContext,
                linkText: string.Empty,
                actionName: Action,
                controllerName: Controller,
                protocol: Protocol,
                hostname: Host,
                fragment: Fragment,
                routeValues: routeValues,
                htmlAttributes: null
            );

            isActiveRoute =
                ViewContext.RouteData.Values["area"]?.ToString() == Area
                && ViewContext.RouteData.Values["controller"]?.ToString() == Controller
                && ViewContext.RouteData.Values["action"]?.ToString() == Action;
        }

        output.MergeAttributes(tagBuilder);

        return isActiveRoute;
    }
}
