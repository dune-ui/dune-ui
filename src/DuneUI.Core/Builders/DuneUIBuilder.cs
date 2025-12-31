using System.ComponentModel;
using DuneUI.Icons;
using DuneUI.Theming;
using Microsoft.Extensions.DependencyInjection;

namespace DuneUI.Builders;

/// <summary>
///     Provides a shared entry point to configure the DuneUI services.
/// </summary>
public class DuneUIBuilder
{
    /// <summary>
    ///     Gets the services collection.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public IServiceCollection Services { get; }

    /// <summary>
    ///     Creates a new instance of <see cref="DuneUIBuilder" />.
    /// </summary>
    /// <param name="services">The services collection.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public DuneUIBuilder(IServiceCollection services)
    {
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }

    /// <summary>
    ///     Adds a custom icon.
    /// </summary>
    /// <param name="name">The name of the icon.</param>
    /// <param name="iconDefinition">The icon definition.</param>
    /// <returns>The <see cref="DuneUIBuilder" /> instance.</returns>
    public DuneUIBuilder AddIcon(string name, IconDefinition iconDefinition)
    {
        DefaultIconManager.Instance.AddIcon(name, iconDefinition);

        return this;
    }

    /// <summary>
    ///     Registers a new icon pack.
    /// </summary>
    /// <typeparam name="TIconPack">The icon pack to register.</typeparam>
    /// <returns>The <see cref="DuneUIBuilder" /> instance.</returns>
    public DuneUIBuilder AddIconPack<TIconPack>()
        where TIconPack : IIconPack, new()
    {
        DefaultIconManager.Instance.AddIconPack<TIconPack>();

        return this;
    }

    /// <summary>
    ///     Configures the style pack used by the application.
    /// </summary>
    /// <typeparam name="TThemePack">The theme pack to use.</typeparam>
    /// <returns>The <see cref="DuneUIBuilder" /> instance.</returns>
    public DuneUIBuilder UseTheme<TThemePack>()
        where TThemePack : IThemePack, new()
    {
        ThemeManager.Instance.UseTheme<TThemePack>();

        return this;
    }
}
