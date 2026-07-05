using DuneUI.Theming;

namespace DuneUI.TagHelpers;

/// <summary>
///     Shared composition for floating menu surfaces (Dropdown Menu content/sub-content, and
///     future Context Menu / Menubar / Navigation Menu families). Turns the resolved
///     <see cref="MenuColor" /> / <see cref="MenuAppearance" /> / <see cref="MenuAccent" />
///     settings into the theme tokens applied to a menu content element.
/// </summary>
/// <remarks>
///     Each accessor returns <c>null</c> when the setting needs no token (Default color, Solid
///     appearance, Subtle accent); <see cref="ICssClassMerger.Merge" /> skips <c>null</c>
///     elements, so the results can be passed straight into a <c>Merge(...)</c> call.
/// </remarks>
internal static class MenuSurfaceInternals
{
    public static ClassElement? ColorToken(MenuColor color) => ToToken(color.GetSurfaceTokenName());

    public static ClassElement? AppearanceToken(MenuAppearance appearance) =>
        ToToken(appearance.GetSurfaceTokenName());

    public static ClassElement? AccentToken(MenuAccent accent) =>
        ToToken(accent.GetSurfaceTokenName());

    private static ClassElement? ToToken(string? tokenName) =>
        tokenName is null ? null : new ThemeToken(tokenName);
}
