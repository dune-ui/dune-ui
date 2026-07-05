using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     An item rendered as an anchor, making the entire row a clickable link.
/// </summary>
[HtmlTargetElement("dui-link-item")]
public class LinkItemTagHelper : DuneUIAnchorTagHelperBase
{
    private readonly IHtmlGenerator _htmlGenerator;

    /// <summary>
    ///     The size of the item, controlling its padding and spacing.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="ItemSize.Default" />.
    /// </remarks>
    [HtmlAttributeName("size")]
    public ItemSize? Size { get; set; }

    /// <summary>
    ///     The visual style of the item.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="ItemVariant.Default" />.
    /// </remarks>
    [HtmlAttributeName("variant")]
    public ItemVariant? Variant { get; set; }

    public LinkItemTagHelper(
        ThemeManager themeManager,
        IHtmlGenerator htmlGenerator,
        ICssClassMerger classMerger
    )
        : base(themeManager, classMerger)
    {
        _htmlGenerator = htmlGenerator ?? throw new ArgumentNullException(nameof(htmlGenerator));
    }

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

        await ItemRenderingHelper.RenderAsync(output, ClassMerger, Size, Variant);
    }
}
