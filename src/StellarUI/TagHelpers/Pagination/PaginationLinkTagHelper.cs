using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-pagination-link")]
public class PaginationLinkTagHelper(IHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
    : StellarAnchorTagHelperBase(htmlGenerator)
{
    [HtmlAttributeName("is-active")]
    public bool IsActive { get; set; } = false;

    [HtmlAttributeName("size")]
    public ButtonSize Size { get; set; } = ButtonSize.Default;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        RenderLink(output);

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }

    protected void RenderLink(TagHelperOutput output)
    {
        ApplyAnchorAttributes(output);

        var paginationLinkRenderer = new PaginationLinkRenderer(classMerger);
        paginationLinkRenderer.Render(output, Size, IsActive);
    }
}
