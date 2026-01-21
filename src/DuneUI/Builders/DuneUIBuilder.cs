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
    ///     Registers a named script. Registered scripts will automatically be rendered before the closing &lt;/body> tag by
    ///     DuneUI at runtime.
    /// </summary>
    /// <param name="name">
    ///     The name of the script. If a script with that name is already registered, it will be replaced with
    ///     the new one.
    /// </param>
    /// <param name="script">The path to the script</param>
    /// <exception cref="ArgumentException"></exception>
    public void RegisterNamedScript(string name, string script)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
        ArgumentNullException.ThrowIfNull(script);

        Services.Configure<DuneUIConfiguration>(config => config.RegisterNamedScript(name, script));
    }

    /// <summary>
    ///     Registers a named stylesheet. Registered stylesheets will automatically be rendered before the closing &lt;/head>
    ///     tag by DuneUI at runtime.
    /// </summary>
    /// <param name="name">
    ///     The name of the stylesheet. If a stylesheet with that name is already registered, it will be
    ///     replaced with the new one.
    /// </param>
    /// <param name="stylesheet">The path to the stylesheet</param>
    /// <exception cref="ArgumentException"></exception>
    public void RegisterNamedStylesheet(string name, string stylesheet)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
        ArgumentNullException.ThrowIfNull(stylesheet);

        Services.Configure<DuneUIConfiguration>(config =>
            config.RegisterNamedStylesheet(name, stylesheet)
        );
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
