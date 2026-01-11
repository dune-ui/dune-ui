using DuneUI.Builders;
using DuneUI.Icons;
using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;

namespace DuneUI;

/// <summary>
///     Extensions for registering and configuring the DuneUI services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Provides the common entry point for registering the DuneUI services.
    /// </summary>
    /// <param name="services">The services collection.</param>
    /// <returns>The <see cref="DuneUIBuilder" /> instances that allows you to register and configure DuneUI services.</returns>
    public static DuneUIBuilder AddDuneUI(this IServiceCollection services)
    {
        services.AddTransient<ITagHelperComponent, DuneUIStylesInjectionTagHelperComponent>();

        var duneUIBuilder = new DuneUIBuilder(services);
        duneUIBuilder.AddCore();
        duneUIBuilder.AddIconPack<LucideIconPack>();
        duneUIBuilder.UseTheme<VegaThemePack>();

        return duneUIBuilder;
    }
}
