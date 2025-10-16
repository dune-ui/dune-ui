using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-pagination-first")]
public class PaginationFirstTagHelper(IHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
    : PaginationLinkTagHelper(htmlGenerator, classMerger)
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        RenderLink(output);

        // // Render the icon
        var iconOutput = new TagHelperOutput(
            "svg",
            [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
        );
        var iconRenderer = new IconRenderer();
        iconRenderer.Render(iconOutput, "chevron-first");
        output.Content.AppendHtml(iconOutput);

        // Render the text
        var textBlockTagBuilder = new TagBuilder("span");
        textBlockTagBuilder.AddCssClass("hidden sm:block");
        textBlockTagBuilder.InnerHtml.AppendHtml("First");
        output.Content.AppendHtml(textBlockTagBuilder);

        return Task.CompletedTask;
    }
}
