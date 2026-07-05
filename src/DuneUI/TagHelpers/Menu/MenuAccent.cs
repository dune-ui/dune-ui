namespace DuneUI.TagHelpers;

/// <summary>
///     The focus/highlight intensity of menu items (Dropdown Menu, and future menu families).
///     Mirrors shadcn's <c>menuAccent</c> setting.
/// </summary>
public enum MenuAccent
{
    /// <summary>A low-contrast highlight (the muted <c>accent</c> color).</summary>
    Subtle,

    /// <summary>A solid highlight in the <c>primary</c> color.</summary>
    Bold,
}

public static class MenuAccentExtensions
{
    extension(MenuAccent accent)
    {
        /// <summary>
        ///     The themepack token that renders this accent, or <c>null</c> when no token is
        ///     needed (<see cref="MenuAccent.Subtle" /> is the absence of a token — the default
        ///     <c>accent</c> highlight already applies).
        /// </summary>
        public string? GetSurfaceTokenName() =>
            accent switch
            {
                MenuAccent.Bold => "dui-menu-accent-bold",
                _ => null,
            };
    }
}
