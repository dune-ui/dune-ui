using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace StellarUI.TagHelpers;

public interface IStellarHtmlGenerator : IHtmlGenerator
{
    TagBuilder GenerateLabel(IDictionary<string, object?>? htmlAttributes = null);

    TagBuilder GenerateTextArea(
        int rows = 0,
        int columns = 0,
        IDictionary<string, object?>? htmlAttributes = null
    );
}
