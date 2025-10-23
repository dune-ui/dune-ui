using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace StellarUI.TagHelpers;

public interface IStellarHtmlGenerator : IHtmlGenerator
{
    TagBuilder GenerateLabel(
        string? labelText = null,
        IDictionary<string, object?>? htmlAttributes = null
    );

    TagBuilder GenerateSelect(
        string? optionLabel,
        IEnumerable<SelectListItem>? selectList,
        ICollection? currentValues,
        bool? allowMultiple = null,
        IDictionary<string, object?>? htmlAttributes = null
    );

    TagBuilder GenerateTextArea(
        int rows = 0,
        int columns = 0,
        IDictionary<string, object?>? htmlAttributes = null
    );

    TagBuilder GenerateValidationMessage(
        string? message = null,
        IDictionary<string, object?>? htmlAttributes = null
    );
}
