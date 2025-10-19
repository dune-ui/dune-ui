using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-textarea")]
public class TextareaTagHelper(IStellarHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
    : StellarTagHelper
{
    private const string ForAttributeName = "asp-for";

    /// <summary>
    /// Gets the <see cref="ViewContext"/> of the executing view.
    /// </summary>
    [HtmlAttributeNotBound]
    [ViewContext]
    public required ViewContext ViewContext { get; set; }

    /// <summary>
    /// An expression to be evaluated against the current model.
    /// </summary>
    [HtmlAttributeName(ForAttributeName)]
    public ModelExpression? For { get; set; }

    /// <summary>
    /// The name of the &lt;input&gt; element.
    /// </summary>
    /// <remarks>
    /// Passed through to the generated HTML in all cases. Also used to determine whether <see cref="For"/> is
    /// valid with an empty <see cref="ModelExpression.Name"/>.
    /// </remarks>
    public string? Name { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "textarea";
        output.TagMode = TagMode.StartTagAndEndTag;

        if (Name != null)
        {
            output.CopyHtmlAttribute(nameof(Name), context);
        }

        IDictionary<string, object?>? htmlAttributes = null;
        if (
            string.IsNullOrEmpty(For?.Name)
            && string.IsNullOrEmpty(ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix)
            && !string.IsNullOrEmpty(Name)
        )
        {
            htmlAttributes = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase)
            {
                { "name", Name },
            };
        }

        var tagBuilder =
            For == null
                ? htmlGenerator.GenerateTextArea(
                    rows: 0,
                    columns: 0,
                    htmlAttributes: htmlAttributes
                )
                : htmlGenerator.GenerateTextArea(
                    ViewContext,
                    For.ModelExplorer,
                    For.Name,
                    rows: 0,
                    columns: 0,
                    htmlAttributes: htmlAttributes
                );

        output.MergeAttributes(tagBuilder);

        output.Attributes.SetAttribute("data-slot", "textarea");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "border-input placeholder:text-muted-foreground focus-visible:border-ring focus-visible:ring-ring/50 aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive dark:bg-input/30 flex field-sizing-content min-h-16 w-full rounded-md border bg-transparent px-3 py-2 text-base shadow-xs transition-[color,box-shadow] outline-none focus-visible:ring-[3px] disabled:cursor-not-allowed disabled:opacity-50 md:text-sm",
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
}
