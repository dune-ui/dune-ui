using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;
using FrameworkAnchorTagHelper = Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-linkbutton")]
public class LinkButtonTagHelper : StellarAnchorTagHelperBase
{
    private readonly IHtmlGenerator _htmlGenerator;
    private readonly ICssClassMerger _classMerger;

    public LinkButtonTagHelper(
        ThemeManager themeManager,
        IHtmlGenerator htmlGenerator,
        ICssClassMerger classMerger
    )
        : base(themeManager)
    {
        _htmlGenerator = htmlGenerator ?? throw new ArgumentNullException(nameof(htmlGenerator));
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    /// <summary>
    ///     The size of the button.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="ButtonSize.Default" />
    /// </remarks>
    [HtmlAttributeName("size")]
    public ButtonSize Size
    {
        get;
        set
        {
            ArgumentNullException.ThrowIfNull(value, nameof(Size));
            field = value;
        }
    } = ButtonSize.Default;

    /// <summary>
    ///     The button variant.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="ButtonVariant.Default" />.
    /// </remarks>
    [HtmlAttributeName("variant")]
    public ButtonVariant Variant
    {
        get;
        set
        {
            ArgumentNullException.ThrowIfNull(value, nameof(Variant));
            field = value;
        }
    } = ButtonVariant.Default;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
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

        ButtonRenderingHelper.RenderAttributes(output, _classMerger, Variant, Size);

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
