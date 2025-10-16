using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-text-input")]
public class TextInputTagHelper(IHtmlGenerator generator) : StellarTagHelper
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
        output.TagName = "input";
        output.TagMode = TagMode.SelfClosing;

        if (For != null)
        {
            var tagBuilder = generator.GenerateTextBox(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                value: null,
                format: null,
                htmlAttributes: null
            );

            output.MergeAttributes(tagBuilder);
        }

        await WrapInFieldIfNecessary(context, output);
    }

    private async Task WrapInFieldIfNecessary(TagHelperContext context, TagHelperOutput output)
    {
        if (IsRenderedInsideField())
        {
            return;
        }

        await RenderLabelContent(context, output);
    }

    private async Task RenderLabelContent(TagHelperContext context, TagHelperOutput output)
    {
        var labelRenderer = new LabelRenderer(generator);
        var labelOutput = new TagHelperOutput(
            "label",
            [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
        );

        await labelRenderer.ProcessAsync(context, labelOutput, ViewContext, For);
        var labelContent = labelOutput.GetContent();

        output.PreElement.AppendHtml(labelContent);
    }

    internal bool IsRenderedInsideField()
    {
        return GetParentTagHelper<FieldTagHelper>() != null;
    }
}
