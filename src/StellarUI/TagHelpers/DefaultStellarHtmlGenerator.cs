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
    public TagBuilder GenerateLabel(IDictionary<string, object?> htmlAttributes)
    {
        var labelTagBuilder = new TagBuilder("label");
        labelTagBuilder.MergeAttributes(htmlAttributes);

        return labelTagBuilder;
    }
}
