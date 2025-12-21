using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;
using FrameworkAnchorTagHelper = Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-pagination-link")]
public class PaginationLinkTagHelper : StellarAnchorTagHelperBase
{
    private readonly IHtmlGenerator _htmlGenerator;

    public PaginationLinkTagHelper(
        ThemeManager themeManager,
        IHtmlGenerator htmlGenerator,
        ICssClassMerger classMerger
    )
        : base(themeManager, classMerger)
    {
        _htmlGenerator = htmlGenerator ?? throw new ArgumentNullException(nameof(htmlGenerator));
    }

    [HtmlAttributeName("is-active")]
    public bool? IsActive { get; set; }

    [HtmlAttributeName("size")]
    public ButtonSize? Size { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveIsActive = IsActive ?? false;
        var effectiveSize = Size ?? ButtonSize.Default;

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

        if (effectiveIsActive)
        {
            output.Attributes.SetAttribute("aria-current", "page");
        }
        output.Attributes.SetAttribute("data-slot", "pagination-link");
        output.Attributes.SetAttribute("data-active", effectiveIsActive ? "true" : "false");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-pagination-link"),
                output.GetUserSuppliedClass()
            )
        );

        ButtonRenderingHelper.RenderAttributes(
            output,
            ClassMerger,
            effectiveIsActive ? ButtonVariant.Outline : ButtonVariant.Ghost,
            effectiveSize
        );
    }
}
