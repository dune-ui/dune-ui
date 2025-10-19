using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

/*
[HtmlTargetElement("sui-label")]
public class LabelTagHelper(IHtmlGenerator generator) : StellarTagHelper
{
    private const string ForAttributeName = "asp-for";

    /// <summary>
    /// An expression to be evaluated against the current model.
    /// </summary>
    [HtmlAttributeName(ForAttributeName)]
    public ModelExpression? For { get; set; }

    /// <summary>
    /// Gets the <see cref="ViewContext"/> of the executing view.
    /// </summary>
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(output);

        var renderer = new LabelRenderer(generator);

        await renderer.ProcessAsync(context, output, ViewContext, For);
    }
}

internal class LabelRenderer(IHtmlGenerator generator)
{
    public async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output,
        ViewContext viewContext,
        ModelExpression? modelExpression
    )
    {
        output.TagName = "label";
        output.TagMode = TagMode.StartTagAndEndTag;

        var childContent = await output.GetChildContentAsync();

        if (modelExpression != null)
        {
            var tagBuilder = generator.GenerateLabel(
                viewContext,
                modelExpression.ModelExplorer,
                modelExpression.Name,
                labelText: null,
                htmlAttributes: null
            );

            output.MergeAttributes(tagBuilder);

            if (childContent.IsEmptyOrWhiteSpace)
            {
                if (tagBuilder.HasInnerHtml)
                {
                    output.Content.SetHtmlContent(tagBuilder.InnerHtml);
                }
            }
        }

        output.Content.AppendHtml(childContent);

        output.PostContent.SetHtmlContent(" <sup>*required</sup>");
    }
}
*/
