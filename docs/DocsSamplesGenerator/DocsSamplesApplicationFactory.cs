using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;

namespace DocsSamplesGenerator;

/// <summary>
/// Hosts the DocsSamples site in-memory so the generator can render pages over an
/// <see cref="System.Net.Http.HttpClient" />. The content root must be set explicitly: outside a test
/// project the <c>Microsoft.AspNetCore.Mvc.Testing</c> build targets do not emit the content-root
/// manifest, so without this override WebApplicationFactory falls back to the bin directory and fails
/// to serve DocsSamples' Razor pages and static (web) assets.
/// </summary>
internal sealed class DocsSamplesApplicationFactory(string contentRoot)
    : WebApplicationFactory<DocsSamples.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseContentRoot(contentRoot);

        // The host's console logging writes directly to stdout, which corrupts Spectre.Console's
        // live progress/status rendering. Drop the console provider and raise the floor to Warning.
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.Warning);
        });
    }
}
