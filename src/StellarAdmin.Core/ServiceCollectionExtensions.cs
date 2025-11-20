using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StellarUI.Builders;
using StellarUI.Icons;
using StellarUI.Icons.Lucide;
using TailwindMerge;

namespace StellarUI;

/// <summary>
///     Extensions for registering and configuring the StellarUI services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Provides the common entry point for registering the StellarUI services.
    /// </summary>
    /// <param name="services">The services collection.</param>
    /// <returns>The <see cref="StellarUIBuilder" /> instances that allows you to register and configure StellarUI services.</returns>
    public static StellarUIBuilder AddStellarUI(this IServiceCollection services)
    {
        DefaultIconManager.Instance.AddIconPack<LucideIconPack>();

        services
            .AddSingleton<TwMerge>()
            .AddSingleton<ICssClassMerger, DefaultCssClassMerger>()
            .AddSingleton<IIconManager>(_ => DefaultIconManager.Instance);

        return new StellarUIBuilder(services);
    }
}
