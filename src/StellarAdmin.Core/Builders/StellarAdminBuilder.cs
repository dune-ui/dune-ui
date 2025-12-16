using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using StellarAdmin.Icons;
using StellarAdmin.Theming;

namespace StellarAdmin.Builders;

/// <summary>
///     Provides a shared entry point to configure the StellarUI services.
/// </summary>
public class StellarAdminBuilder
{
    /// <summary>
    ///     Gets the services collection.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public IServiceCollection Services { get; }

    /// <summary>
    ///     Creates a new instance of <see cref="StellarAdminBuilder" />.
    /// </summary>
    /// <param name="services">The services collection.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public StellarAdminBuilder(IServiceCollection services)
    {
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }

    /// <summary>
    ///     Adds a custom icon.
    /// </summary>
    /// <param name="name">The name of the icon.</param>
    /// <param name="shapes">The shapes that represent the icon.</param>
    /// <returns>The <see cref="StellarAdminBuilder" /> instance.</returns>
    public StellarAdminBuilder AddIcon(string name, List<SvgShape> shapes)
    {
        DefaultIconManager.Instance.AddIcon(name, shapes);

        return this;
    }

    /// <summary>
    ///     Registers a new icon pack.
    /// </summary>
    /// <typeparam name="TIconPack">The icon pack to register.</typeparam>
    /// <returns>The <see cref="StellarAdminBuilder" /> instance.</returns>
    public StellarAdminBuilder AddIconPack<TIconPack>()
        where TIconPack : IIconPack, new()
    {
        DefaultIconManager.Instance.AddIconPack<TIconPack>();

        return this;
    }

    /// <summary>
    ///     Configures the style pack used by the application.
    /// </summary>
    /// <typeparam name="TThemePack">The theme pack to use.</typeparam>
    /// <returns>The <see cref="StellarAdminBuilder" /> instance.</returns>
    public StellarAdminBuilder UseTheme<TThemePack>()
        where TThemePack : IThemePack, new()
    {
        ThemeManager.Instance.UseTheme<TThemePack>();

        return this;
    }
}
