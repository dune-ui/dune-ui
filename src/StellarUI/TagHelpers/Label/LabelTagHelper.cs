using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-label")]
public class LabelTagHelper(IStellarHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
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
        var labelRenderer = new LabelRenderer(htmlGenerator, classMerger);
        await labelRenderer.Render(context, output, ViewContext, For);
    }
}

internal class LabelRenderer(IStellarHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
{
    public async Task Render(
        TagHelperContext context,
        TagHelperOutput output,
        ViewContext viewContext,
        ModelExpression? modelExpression
    )
    {
        output.TagName = "label";
        output.TagMode = TagMode.StartTagAndEndTag;

        var htmlAttributes = new Dictionary<string, object?>
        {
            // ["data-slot"] = output.Attributes.TryGetAttribute("data-slot", out var slot)
            //     ? slot.Value
            //     : "label",
            // ["class"] = classMerger.Merge(
            //     "flex items-center gap-2 text-sm leading-none font-medium select-none group-data-[disabled=true]:pointer-events-none group-data-[disabled=true]:opacity-50 peer-disabled:cursor-not-allowed peer-disabled:opacity-50",
            //     output.GetUserSuppliedClass()
            // ),
        };
        var tagBuilder =
            modelExpression == null
                ? htmlGenerator.GenerateLabel(htmlAttributes)
                : htmlGenerator.GenerateLabel(
                    viewContext,
                    modelExpression.ModelExplorer,
                    modelExpression.Name,
                    labelText: null,
                    htmlAttributes: htmlAttributes
                );

        output.MergeAttributes(tagBuilder);

        if (!output.Attributes.ContainsName("data-slot"))
        {
            output.Attributes.SetAttribute("data-slot", "label");
        }
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "flex items-center gap-2 text-sm leading-none font-medium select-none group-data-[disabled=true]:pointer-events-none group-data-[disabled=true]:opacity-50 peer-disabled:cursor-not-allowed peer-disabled:opacity-50",
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
