using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace StellarUI.TagHelpers;

public interface IStellarHtmlGenerator
{
    TagBuilder GenerateLabel(
        ViewContext viewContext,
        ModelExplorer? modelExplorer,
        string? expression,
        string? labelText,
        string? htmlAttributes
    );
}
