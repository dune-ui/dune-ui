using System.Collections.Frozen;

namespace StellarAdmin.Theming;

public sealed class ThemeManager
{
    private FrozenDictionary<string, string> _componentClassLookup = FrozenDictionary<
        string,
        string
    >.Empty;

    public static ThemeManager Instance { get; } = new ThemeManager();

    private ThemeManager() { }

    internal void UseTheme<TThemePack>()
        where TThemePack : IThemePack, new()
    {
        var themePack = new TThemePack();

        _componentClassLookup =
            themePack.GetComponentClasses()?.ToFrozenDictionary()
            ?? FrozenDictionary<string, string>.Empty;
    }

    public string GetComponentClass(string name)
    {
        return _componentClassLookup.TryGetValue(name, out var result) ? result : string.Empty;
    }
}
