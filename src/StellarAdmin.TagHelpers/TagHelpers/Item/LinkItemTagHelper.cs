using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-link-item")]
public class LinkItemTagHelper : StellarAnchorTagHelperBase
{
    private readonly IHtmlGenerator _htmlGenerator;
    private readonly ICssClassMerger _classMerger;

    public LinkItemTagHelper(
        ThemeManager themeManager,
        IHtmlGenerator htmlGenerator,
        ICssClassMerger classMerger
    )
        : base(themeManager)
    {
        _htmlGenerator = htmlGenerator ?? throw new ArgumentNullException(nameof(htmlGenerator));
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    [HtmlAttributeName("size")]
    public ItemSize Size { get; set; } = ItemSize.Default;

    [HtmlAttributeName("variant")]
    public ItemVariant Variant { get; set; } = ItemVariant.Default;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
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

        var itemRenderer = new ItemRenderer(_classMerger);
        await itemRenderer.RenderAsync(context, output, Size, Variant);
    }
}
