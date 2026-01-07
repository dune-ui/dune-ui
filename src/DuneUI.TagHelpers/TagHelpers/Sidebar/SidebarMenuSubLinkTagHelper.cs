using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-sidebar-menu-sub-link")]
public class SidebarMenuSubLinkTagHelper : DuneUIAnchorTagHelperBase
{
    private readonly IHtmlGenerator _htmlGenerator;

    public SidebarMenuSubLinkSize? Size { get; set; }

    public SidebarMenuSubLinkTagHelper(
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
        var effectiveSize = Size ?? SidebarMenuSubLinkSize.Medium;

        output.TagName = "a";
        output.TagMode = TagMode.StartTagAndEndTag;

        var anchorTagHelper = new AnchorTagHelper(_htmlGenerator)
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

        output.Attributes.SetAttribute("data-slot", "sidebar-menu-sub-button");
        output.Attributes.SetAttribute("data-sidebar", "menu-sub-button");
        output.Attributes.SetAttribute("data-size", effectiveSize.GetDataAttributeText());
        if (IsActiveRoute())
        {
            output.Attributes.SetAttribute("data-active", true);
        }

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-sidebar-menu-sub-button"),
                "flex min-w-0 -translate-x-px items-center overflow-hidden outline-hidden group-data-[collapsible=icon]:hidden disabled:pointer-events-none disabled:opacity-50 aria-disabled:pointer-events-none aria-disabled:opacity-50 [&>span:last-child]:truncate [&>svg]:shrink-0",
                output.GetUserSuppliedClass()
            )
        );
    }
}
