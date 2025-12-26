using System.Collections.Frozen;
using System.Text;

namespace DuneUI.Theming;

public sealed class ThemeManager
{
    private FrozenDictionary<string, string> _componentClassLookup = FrozenDictionary<
        string,
        string
    >.Empty;

    public static ThemeManager Instance { get; } = new ThemeManager();

    private ThemeManager() { }

    public string GetComponentClass(string name)
    {
        return _componentClassLookup.TryGetValue(name, out var result) ? result : string.Empty;
    }

    internal void UseTheme<TThemePack>()
        where TThemePack : IThemePack, new()
    {
        var themePack = new TThemePack();

        _componentClassLookup =
            themePack.GetComponentClasses()?.ToFrozenDictionary()
            ?? FrozenDictionary<string, string>.Empty;
    }

    private KeyValuePair<string, string> Transform(KeyValuePair<string, string> input)
    {
        return input.Key switch
        {
            "dui-native-select" => AddInputValidationErrorVariantsFromAriaInvalid(
                input.Key,
                input.Value
            ),
            _ => input,
        };
    }

    private KeyValuePair<string, string> AddInputValidationErrorVariantsFromAriaInvalid(
        string key,
        string value
    )
    {
        var sb = new StringBuilder();

        var individualClasses = value.Split(
            " ",
            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries
        );

        foreach (var individualClass in individualClasses)
        {
            sb.Append(individualClass);
            sb.Append(" ");

            if (individualClass.Contains("aria-invalid:"))
            {
                sb.Append(individualClass.Replace("aria-invalid:", "[&.input-validation-error]:"));
                sb.Append(" ");
            }
        }

        return new KeyValuePair<string, string>(key, sb.ToString());
    }
}
