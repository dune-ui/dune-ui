using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;
using FrameworkAnchorTagHelper = Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-pagination-link")]
public class PaginationLinkTagHelper : StellarAnchorTagHelperBase
{
    private readonly IHtmlGenerator _htmlGenerator;
    private readonly ICssClassMerger _classMerger;

    public PaginationLinkTagHelper(
        ThemeManager themeManager,
        IHtmlGenerator htmlGenerator,
        ICssClassMerger classMerger
    )
        : base(themeManager)
    {
        _htmlGenerator = htmlGenerator ?? throw new ArgumentNullException(nameof(htmlGenerator));
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    [HtmlAttributeName("is-active")]
    public bool IsActive { get; set; } = false;

    [HtmlAttributeName("size")]
    public ButtonSize Size { get; set; } = ButtonSize.Default;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        await RenderLink(context, output);

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }

    protected async Task RenderLink(TagHelperContext context, TagHelperOutput output)
    {
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

        if (IsActive)
        {
            output.Attributes.SetAttribute("aria-current", "page");
        }
        output.Attributes.SetAttribute("data-slot", "pagination-link");
        output.Attributes.SetAttribute("data-active", IsActive.ToString().ToLower());

        ButtonRenderingHelper.RenderAttributes(
            output,
            _classMerger,
            IsActive ? ButtonVariant.Outline : ButtonVariant.Ghost,
            Size
        );
    }
}
