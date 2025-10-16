using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-pagination-ellipsis")]
public class PaginationEllipsisTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("aria-hidden", "true");
        output.Attributes.SetAttribute("data-slot", "pagination-ellipsis");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "flex size-9 items-center justify-center",
                output.GetUserSuppliedClass()
            )
        );

        // Render the icon
        var iconOutput = new TagHelperOutput(
            "svg",
            [new TagHelperAttribute("class", "size-4")],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
        );
        var iconRenderer = new IconRenderer();
        iconRenderer.Render(iconOutput, "ellipsis");
        output.Content.AppendHtml(iconOutput);

        // Render the text
        var textBlockTagBuilder = new TagBuilder("span");
        textBlockTagBuilder.AddCssClass("sr-only");
        textBlockTagBuilder.InnerHtml.AppendHtml("More pages");
        output.Content.AppendHtml(textBlockTagBuilder);

        return Task.CompletedTask;
    }
}
