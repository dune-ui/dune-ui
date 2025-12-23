using DuneUI.Theming;
using TailwindMerge;

namespace DuneUI;

internal class DefaultCssClassMerger : ICssClassMerger
{
    private readonly ThemeManager _themeManager;
    private readonly TwMerge _twMerge;

    public DefaultCssClassMerger(ThemeManager themeManager, TwMerge twMerge)
    {
        _themeManager = themeManager ?? throw new ArgumentNullException(nameof(themeManager));
        _twMerge = twMerge ?? throw new ArgumentNullException(nameof(twMerge));
    }

    public string? Merge(params ClassElement?[] classes)
    {
        return _twMerge.Merge(
            classes
                .Select(c =>
                {
                    return c switch
                    {
                        ComponentName cn => _themeManager.GetComponentClass(cn.Name),
                        ClassList cl => cl.Classes,
                        _ => string.Empty,
                    };
                })
                .ToArray()
        );
    }
}
