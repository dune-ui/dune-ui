using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace StellarUI.TagHelpers;

public class DefaultStellarHtmlGenerator : IStellarHtmlGenerator
{
    public TagBuilder GenerateLabel(
        ViewContext viewContext,
        ModelExplorer? modelExplorer,
        string? expression,
        string? labelText,
        string? htmlAttributes
    )
    {
        ArgumentNullException.ThrowIfNull(viewContext, nameof(viewContext));

        var unencoded =
            labelText
            ?? modelExplorer?.Metadata.DisplayName
            ?? modelExplorer?.Metadata.PropertyName;

        if (unencoded == null && expression != null)
        {
            var num = expression.LastIndexOf('.');
            unencoded = num != -1 ? expression.Substring(num + 1) : expression;
        }

        var label = new TagBuilder("label");
        // string fullHtmlFieldName = NameAndIdProvider.GetFullHtmlFieldName(viewContext, expression);

        return label;
    }
}
