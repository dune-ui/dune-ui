using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using FrameworkAnchorTagHelper = Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper;

namespace DuneUI.TagHelpers;

/// <summary>
///     A selectable menu item. Renders as a
///     <c>&lt;div role="menuitem"&gt;</c>, or as an <c>&lt;a role="menuitem"&gt;</c> when the author
///     supplies a URL — either a raw <c>href</c> or ASP.NET routing attributes (<c>asp-page</c>,
///     <c>asp-action</c>/<c>asp-controller</c>, <c>asp-route-*</c>, …). The <c>del-dropdown-menu</c>
///     web component activates items by <c>role</c>, so both elements behave identically.
/// </summary>
[HtmlTargetElement("dui-dropdown-menu-item")]
public class DropdownMenuItemTagHelper : DuneUIAnchorTagHelperBase
{
    private readonly IHtmlGenerator _htmlGenerator;

    public DropdownMenuItemTagHelper(
        ThemeManager themeManager,
        ICssClassMerger classMerger,
        IHtmlGenerator htmlGenerator
    )
        : base(themeManager, classMerger)
    {
        _htmlGenerator = htmlGenerator ?? throw new ArgumentNullException(nameof(htmlGenerator));
    }

    [HtmlAttributeName("close-on-click")]
    public bool? CloseOnClick { get; set; }

    [HtmlAttributeName("disabled")]
    public bool? Disabled { get; set; }

    [HtmlAttributeName("href")]
    public string? Href { get; set; }

    [HtmlAttributeName("inset")]
    public bool? Inset { get; set; }

    [HtmlAttributeName("variant")]
    public DropdownMenuItemVariant? Variant { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveVariant = Variant ?? DropdownMenuItemVariant.Default;

        // A routing target (asp-page / asp-action / asp-controller / asp-route / asp-area /
        // asp-page-handler / asp-route-*) makes the item a link the framework resolves; a raw href
        // does too. asp-fragment/host/protocol are modifiers and only apply alongside a target.
        var hasRouteTarget =
            Page != null
            || Action != null
            || Controller != null
            || Route != null
            || Area != null
            || PageHandler != null
            || RouteValues.Count > 0;
        var isLink = Href != null || hasRouteTarget;

        output.TagName = isLink ? "a" : "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        if (hasRouteTarget)
        {
            // Let the framework anchor helper emit the href from the routing attributes.
            await new FrameworkAnchorTagHelper(_htmlGenerator)
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
            }.ProcessAsync(context, output);
        }
        else if (Href != null)
        {
            output.Attributes.SetAttribute("href", Href);
        }

        output.Attributes.SetAttribute("role", "menuitem");
        output.Attributes.SetAttribute("tabindex", "-1");
        output.Attributes.SetAttribute("data-slot", "dropdown-menu-item");
        output.Attributes.SetAttribute("data-variant", effectiveVariant.GetDataAttributeText());
        if (Inset == true)
        {
            output.Attributes.SetAttribute("data-inset", "true");
        }

        // Only emit when the author overrides the default (plain items close on click).
        if (CloseOnClick.HasValue)
        {
            output.Attributes.SetAttribute(
                "data-close-on-click",
                CloseOnClick.Value ? "true" : "false"
            );
        }

        if (Disabled == true)
        {
            output.Attributes.SetAttribute("data-disabled", "");
            output.Attributes.SetAttribute("aria-disabled", "true");
        }

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-dropdown-menu-item"),
                "group/dropdown-menu-item relative flex cursor-default items-center outline-hidden select-none data-disabled:pointer-events-none data-disabled:opacity-50 [&_svg]:pointer-events-none [&_svg]:shrink-0",
                output.GetUserSuppliedClass()
            )
        );
    }
}
