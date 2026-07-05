namespace DuneUI.TagHelpers;

/// <summary>
///     The color scheme of a floating menu surface (Dropdown Menu content, and future menu
///     families).
/// </summary>
public enum MenuColor
{
    /// <summary>The menu renders in the current color scheme (opaque <c>popover</c> surface).</summary>
    Default,

    /// <summary>The menu surface renders in the dark color scheme regardless of the page scheme.</summary>
    Inverted,
}

public static class MenuColorExtensions
{
    extension(MenuColor color)
    {
        /// <summary>
        ///     The themepack token that renders this color, or <c>null</c> when no token is
        ///     needed (<see cref="MenuColor.Default" /> is the absence of a token).
        /// </summary>
        public string? GetSurfaceTokenName() =>
            color switch
            {
                MenuColor.Inverted => "dui-menu-inverted",
                _ => null,
            };
    }
}
