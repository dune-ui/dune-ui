using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace StellarUI.TagHelpers;

public interface IStellarHtmlGenerator : IHtmlGenerator
{
    TagBuilder GenerateLabel(IDictionary<string, object?> htmlAttributes);
}
