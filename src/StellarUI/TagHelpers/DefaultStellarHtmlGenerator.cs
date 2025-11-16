using System.Globalization;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;

namespace StellarUI.TagHelpers;

public class DefaultStellarHtmlGenerator(
    IAntiforgery antiforgery,
    IOptions<MvcViewOptions> optionsAccessor,
    IModelMetadataProvider metadataProvider,
    IUrlHelperFactory urlHelperFactory,
    HtmlEncoder htmlEncoder,
    ValidationHtmlAttributeProvider validationAttributeProvider
)
    : DefaultHtmlGenerator(
        antiforgery,
        optionsAccessor,
        metadataProvider,
        urlHelperFactory,
        htmlEncoder,
        validationAttributeProvider
    ),
        IStellarHtmlGenerator
{
    public TagBuilder GenerateTextArea(
        int rows = 0,
        int columns = 0,
        IDictionary<string, object?>? htmlAttributes = null
    )
    {
        if (rows < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rows));
        }

        if (columns < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(columns));
        }

        var tagBuilder = new TagBuilder("textarea");
        if (htmlAttributes != null)
        {
            tagBuilder.MergeAttributes(htmlAttributes, true);
        }

        if (rows > 0)
        {
            tagBuilder.MergeAttribute("rows", rows.ToString(CultureInfo.InvariantCulture), true);
        }

        if (columns > 0)
        {
            tagBuilder.MergeAttribute("cols", columns.ToString(CultureInfo.InvariantCulture), true);
        }

        return tagBuilder;
    }

    public TagBuilder GenerateValidationMessage(IDictionary<string, object?>? htmlAttributes = null)
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
