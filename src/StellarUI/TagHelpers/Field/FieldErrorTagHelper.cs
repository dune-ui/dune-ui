using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-field-error")]
public class FieldErrorTagHelper(IStellarHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
    : StellarTagHelper
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
        var renderer = new FieldErrorRenderer(htmlGenerator, classMerger);
        await renderer.Render(output, ViewContext, For, null);
    }
}

internal class FieldErrorRenderer(IStellarHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
{
    public async Task Render(
        TagHelperOutput output,
        ViewContext viewContext,
        ModelExpression? modelExpression,
        string? message
    )
    {
        var childContent = await output.GetChildContentAsync();
        if (!childContent.IsEmptyOrWhiteSpace)
        {
            message = childContent.GetContent();
        }

        var tagBuilder =
            modelExpression == null
                ? htmlGenerator.GenerateValidationMessage(message, null)
                : htmlGenerator.GenerateValidationMessage(
                    viewContext,
                    modelExpression.ModelExplorer,
                    modelExpression.Name,
                    message,
                    "div",
                    null
                );

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.MergeAttributes(tagBuilder);

        output.Attributes.SetAttribute("role", "alert");
        output.Attributes.SetAttribute("data-slot", "field-error");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "text-destructive text-sm font-normal",
                "hidden [&.field-validation-error]:block",
                output.GetUserSuppliedClass()
            )
        );

        if (tagBuilder.HasInnerHtml)
        {
            output.Content.SetHtmlContent(tagBuilder.InnerHtml);
        }
    }
}
