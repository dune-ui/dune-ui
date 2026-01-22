using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DuneUI;

internal sealed class StyleInjectionTagHelperComponent : TagHelperComponent
{
    private DuneUIConfiguration _duneUIConfiguration;

    public StyleInjectionTagHelperComponent(IOptions<DuneUIConfiguration> duneUIConfiguration)
    {
        _duneUIConfiguration =
            duneUIConfiguration.Value
            ?? throw new ArgumentNullException(nameof(duneUIConfiguration));
    }

    [ViewContext]
    public ViewContext ViewContext { get; set; } = default!;

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (!string.Equals(context.TagName, "head", StringComparison.OrdinalIgnoreCase))
        {
            return Task.CompletedTask;
        }

        var httpContext = ViewContext.HttpContext;
        var fileVersionProvider =
            httpContext.RequestServices.GetRequiredService<IFileVersionProvider>();
        var pathBase = httpContext.Request.PathBase;

        foreach (var stylesheet in _duneUIConfiguration.GetStylesheets())
        {
            var href = stylesheet.StartsWith("/")
                ? fileVersionProvider.AddFileVersionToPath(
                    pathBase,
                    UriHelper.BuildRelative(pathBase, stylesheet)
                )
                : stylesheet;

            output.PostContent.AppendHtml("<link rel=\"stylesheet\" href=\"");
            output.PostContent.Append(href);
            output.PostContent.AppendHtmlLine("\">");
        }

        return Task.CompletedTask;
    }
}
