using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;

namespace DuneUI;

internal sealed class DuneUIStylesInjectionTagHelperComponent : TagHelperComponent
{
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
        var duneUIStylesSrc = fileVersionProvider.AddFileVersionToPath(
            pathBase,
            UriHelper.BuildRelative(pathBase, "/_content/DuneUI.TagHelpers/dune-ui.css")
        );

        output.PostContent.AppendHtml("<link rel=\"stylesheet\" href=\"");
        output.PostContent.Append(duneUIStylesSrc);
        output.PostContent.AppendHtml("\">");

        return Task.CompletedTask;
    }
}
