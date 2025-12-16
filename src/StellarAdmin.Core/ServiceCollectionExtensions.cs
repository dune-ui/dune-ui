using Microsoft.Extensions.DependencyInjection;
using StellarAdmin.Builders;
using StellarAdmin.Icons;
using StellarAdmin.Theming;
using TailwindMerge;

namespace StellarAdmin;

/// <summary>
///     Extensions for registering and configuring the StellarUI services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Provides the common entry point for registering the StellarUI services.
    /// </summary>
    /// <param name="services">The services collection.</param>
    /// <returns>The <see cref="StellarAdminBuilder" /> instances that allows you to register and configure StellarUI services.</returns>
    public static StellarAdminBuilder AddStellarAdmin(this IServiceCollection services)
    {
        services
            .AddSingleton<TwMerge>()
            .AddSingleton<ICssClassMerger, DefaultCssClassMerger>()
            .AddSingleton<IIconManager>(_ => DefaultIconManager.Instance)
            .AddSingleton<ThemeManager>(_ => ThemeManager.Instance);

        var stellarAdminBuilder = new StellarAdminBuilder(services);
        stellarAdminBuilder.AddIconPack<LucideIconPack>();
        stellarAdminBuilder.UseTheme<VegaThemePack>();

        return stellarAdminBuilder;
    }
}
