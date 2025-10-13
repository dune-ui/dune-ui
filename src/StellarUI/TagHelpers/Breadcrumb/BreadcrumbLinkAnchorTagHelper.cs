using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-breadcrumb-link", Attributes = Href)]
[HtmlTargetElement("sui-breadcrumb-link", Attributes = ActionAttributeName)]
[HtmlTargetElement("sui-breadcrumb-link", Attributes = ControllerAttributeName)]
[HtmlTargetElement("sui-breadcrumb-link", Attributes = AreaAttributeName)]
[HtmlTargetElement("sui-breadcrumb-link", Attributes = PageAttributeName)]
[HtmlTargetElement("sui-breadcrumb-link", Attributes = PageHandlerAttributeName)]
[HtmlTargetElement("sui-breadcrumb-link", Attributes = FragmentAttributeName)]
[HtmlTargetElement("sui-breadcrumb-link", Attributes = HostAttributeName)]
[HtmlTargetElement("sui-breadcrumb-link", Attributes = ProtocolAttributeName)]
[HtmlTargetElement("sui-breadcrumb-link", Attributes = RouteAttributeName)]
[HtmlTargetElement("sui-breadcrumb-link", Attributes = RouteValuesDictionaryName)]
[HtmlTargetElement("sui-breadcrumb-link", Attributes = RouteValuesPrefix + "*")]
public class BreadcrumbLinkAnchorTagHelper(IHtmlGenerator generator) : AnchorTagHelper(generator)
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
}
