using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using FrameworkAnchorTagHelper = Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-sidebar-menu-link")]
public class SidebarMenuLinkTagHelper : DuneUIAnchorTagHelperBase
{
    private static readonly Dictionary<SidebarMenuLinkSize, ComponentName> SizeClasses = new()
    {
        [SidebarMenuLinkSize.Default] = new ComponentName("dui-sidebar-menu-button-size-default"),
        [SidebarMenuLinkSize.Small] = new ComponentName("dui-sidebar-menu-button-size-sm"),
        [SidebarMenuLinkSize.Large] = new ComponentName("dui-sidebar-menu-button-size-lg"),
    };

    private static readonly Dictionary<SidebarMenuLinkVariant, ComponentName> VariantClasses = new()
    {
        [SidebarMenuLinkVariant.Default] = new ComponentName(
            "dui-sidebar-menu-button-variant-default"
        ),
        [SidebarMenuLinkVariant.Outline] = new ComponentName(
            "dui-sidebar-menu-button-variant-outline"
        ),
    };

    private readonly IHtmlGenerator _htmlGenerator;

    public SidebarMenuLinkSize? Size { get; set; }

    public SidebarMenuLinkVariant? Variant { get; set; }

    public SidebarMenuLinkTagHelper(
        ThemeManager themeManager,
        ICssClassMerger classMerger,
        IHtmlGenerator htmlGenerator
    )
        : base(themeManager, classMerger)
    {
        _htmlGenerator = htmlGenerator ?? throw new ArgumentNullException(nameof(htmlGenerator));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveSize = Size ?? SidebarMenuLinkSize.Default;
        var effectiveVariant = Variant ?? SidebarMenuLinkVariant.Default;

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

        output.Attributes.SetAttribute("data-slot", "sidebar-menu-button");
        output.Attributes.SetAttribute("data-sidebar", "menu-button");
        output.Attributes.SetAttribute("data-size", effectiveSize.GetDataAttributeText());
        if (IsActiveRoute())
        {
            output.Attributes.SetAttribute("data-active", null);
        }

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-sidebar-menu-button"),
                "peer/menu-button flex w-full items-center  overflow-hidden outline-hidden group/menu-button disabled:pointer-events-none disabled:opacity-50 aria-disabled:pointer-events-none aria-disabled:opacity-50 [&>span:last-child]:truncate [&_svg]:size-4 [&_svg]:shrink-0",
                SizeClasses[effectiveSize],
                VariantClasses[effectiveVariant],
                output.GetUserSuppliedClass()
            )
        );
    }
}
