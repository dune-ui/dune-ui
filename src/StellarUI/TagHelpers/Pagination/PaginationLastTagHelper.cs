using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-pagination-last")]
public class PaginationLastTagHelper(IHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
    : PaginationLinkTagHelper(htmlGenerator, classMerger)
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        await RenderLink(context, output);

        // Render the text
        var textBlockTagBuilder = new TagBuilder("span");
        textBlockTagBuilder.AddCssClass("hidden sm:block");
        textBlockTagBuilder.InnerHtml.AppendHtml("Last");
        output.Content.AppendHtml(textBlockTagBuilder);

        // Render the icon
        var iconOutput = new TagHelperOutput(
            "svg",
            [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
        );
        var iconRenderer = new IconRenderer();
        iconRenderer.Render(iconOutput, "chevron-last");
        output.Content.AppendHtml(iconOutput);
    }
}
