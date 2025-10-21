using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

internal class LabelRenderer(IStellarHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
{
    public async Task Render(
        TagHelperOutput output,
        ViewContext viewContext,
        ModelExpression? modelExpression,
        string? labelText
    )
    {
        output.TagName = "label";
        output.TagMode = TagMode.StartTagAndEndTag;

        var tagBuilder =
            modelExpression == null
                ? htmlGenerator.GenerateLabel(labelText: labelText)
                : htmlGenerator.GenerateLabel(
                    viewContext,
                    modelExpression.ModelExplorer,
                    modelExpression.Name,
                    labelText: labelText,
                    htmlAttributes: null
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
