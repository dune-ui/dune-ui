using System.Collections.Frozen;
using System.Collections.Immutable;

namespace DuneUI.Icons;

internal static partial class LucideIcons
{
    private static readonly Dictionary<string, string> SvgAttributes = new Dictionary<
        string,
        string
    >
    {
        ["xmlns"] = "http://www.w3.org/2000/svg",
        ["width"] = "24",
        ["height"] = "24",
        ["viewBox"] = "0 0 24 24",
        ["fill"] = "none",
        ["stroke"] = "currentColor",
        ["stroke-width"] = "2",
        ["stroke-linecap"] = "round",
        ["stroke-linejoin"] = "round",
    };

    public static List<SvgShape> xIconAArrowDown =
    [
        new SvgShape(
            "path",
            new Dictionary<string, string> { ["d"] = "m14 12 4 4 4-4" }.ToImmutableDictionary()
        ),
        new SvgShape(
            "path",
            new Dictionary<string, string> { ["d"] = "M18 16V7" }.ToImmutableDictionary()
        ),
        new SvgShape(
            "path",
            new Dictionary<string, string>
            {
                ["d"] = "m2 16 4.039-9.69a.5.5 0 0 1 .923 0L11 16",
            }.ToImmutableDictionary()
        ),
        new SvgShape(
            "path",
            new Dictionary<string, string> { ["d"] = "M3.304 13h6.392" }.ToImmutableDictionary()
        ),
    ];

    public static FrozenDictionary<string, IconDefinition> xIconDefinitions = new Dictionary<
        string,
        IconDefinition
    >
    {
        ["xxx"] = new IconDefinition(SvgAttributes.ToImmutableDictionary(), xIconAArrowDown),
    }.ToFrozenDictionary();
}
