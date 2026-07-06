namespace DuneUI.TagHelpers;

/// <summary>
///     The surface treatment of a floating menu (Dropdown Menu content, and future menu
///     families).
/// </summary>
public enum MenuAppearance
{
    /// <summary>An opaque <c>popover</c> surface.</summary>
    Solid,

    /// <summary>A frosted "liquid glass" surface (backdrop blur over a translucent background).</summary>
    Translucent,
}

internal static class MenuAppearanceExtensions
{
    extension(MenuAppearance appearance)
    {
        /// <summary>
        ///     The themepack token that renders this appearance, or <c>null</c> when no token is
        ///     needed (<see cref="MenuAppearance.Solid" /> is the absence of a token).
        /// </summary>
        public string? GetSurfaceTokenName() =>
            appearance switch
            {
                MenuAppearance.Translucent => "dui-menu-translucent",
                _ => null,
            };
    }
}
