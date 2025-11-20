using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarUI.Icons;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sa-pagination-first")]
public class PaginationFirstTagHelper(
    IHtmlGenerator htmlGenerator,
    ICssClassMerger classMerger,
    IIconManager iconManager
) : PaginationLinkTagHelper(htmlGenerator, classMerger)
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        await RenderLink(context, output);

        var content = await output.GetChildContentAsync();

        if (!content.IsEmptyOrWhiteSpace)
        {
            output.Content.AppendHtml(content);
        }
        else
        {
            // Render the icon
            var iconOutput = new TagHelperOutput(
                "svg",
                [],
                (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
            );
            var iconTagHelper = new IconTagHelper(iconManager) { Name = "chevron-first" };
            await iconTagHelper.ProcessAsync(context, iconOutput);
            output.Content.AppendHtml(iconOutput);

            // Render the text
            var textBlockTagBuilder = new TagBuilder("span");
            textBlockTagBuilder.AddCssClass("hidden sm:block");
            textBlockTagBuilder.InnerHtml.AppendHtml("First");
            output.Content.AppendHtml(textBlockTagBuilder);
        }
    }
}
