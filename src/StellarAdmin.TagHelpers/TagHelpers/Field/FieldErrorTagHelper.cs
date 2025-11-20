using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-field-error")]
public class FieldErrorTagHelper(IHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
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
    public required ViewContext ViewContext { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var tagBuilder =
            For == null
                ? GenerateValidationMessageTagBuilder()
                : htmlGenerator.GenerateValidationMessage(
                    ViewContext,
                    For.ModelExplorer,
                    For.Name,
                    null,
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

        var childContent = await output.GetChildContentAsync();
        if (childContent.IsEmptyOrWhiteSpace)
        {
            if (tagBuilder.HasInnerHtml)
            {
                output.Content.SetHtmlContent(tagBuilder.InnerHtml);
            }
        }
        else
        {
            output.Content.SetHtmlContent(childContent);
        }
    }

    private TagBuilder GenerateValidationMessageTagBuilder(
        IDictionary<string, object?>? htmlAttributes = null
    )
    {
        var tagBuilder = new TagBuilder("div");
        tagBuilder.AddCssClass(HtmlHelper.ValidationMessageCssClassName);

        if (htmlAttributes != null)
        {
            tagBuilder.MergeAttributes(htmlAttributes);
        }

        return tagBuilder;
    }
}
