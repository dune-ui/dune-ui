using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DuneUI;

public class ScriptInjectionTagHelperComponent : TagHelperComponent
{
    private DuneUIConfiguration _duneUIConfiguration;

    public ScriptInjectionTagHelperComponent(IOptions<DuneUIConfiguration> duneUIConfiguration)
    {
        _duneUIConfiguration =
            duneUIConfiguration.Value
            ?? throw new ArgumentNullException(nameof(duneUIConfiguration));
    }

    [ViewContext]
    public ViewContext ViewContext { get; set; } = default!;

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (!string.Equals(context.TagName, "body", StringComparison.OrdinalIgnoreCase))
        {
            return Task.CompletedTask;
        }

        var httpContext = ViewContext.HttpContext;
        var fileVersionProvider =
            httpContext.RequestServices.GetRequiredService<IFileVersionProvider>();
        var pathBase = httpContext.Request.PathBase;

        foreach (var script in _duneUIConfiguration.GetScripts())
        {
            var href = script.StartsWith("/")
                ? fileVersionProvider.AddFileVersionToPath(
                    pathBase,
                    UriHelper.BuildRelative(pathBase, script)
                )
                : script;

            output.PostContent.AppendHtml("<script src=\"");
            output.PostContent.Append(href);
            output.PostContent.AppendHtmlLine("\"></script>");
        }

        return Task.CompletedTask;
    }
}
