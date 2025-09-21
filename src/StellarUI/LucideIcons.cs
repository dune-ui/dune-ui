using System.Collections.Frozen;
using System.Collections.Immutable;

namespace StellarUI;

internal static partial class LucideIcons
{
    public static List<SvgShape> IconAbc =
    [
        new SvgShape(
            "path",
            new Dictionary<string, string>
            {
                ["d"] = "M15.697 14h5.606",
                ["e"] = "M15.697 14h5.606",
            }.ToImmutableDictionary()
        ),
    ];

    public static FrozenDictionary<string, List<SvgShape>> IconDefinitions2 = new Dictionary<
        string,
        List<SvgShape>
    >
    {
        ["abc"] = IconAbc,
    }.ToFrozenDictionary();

    public record SvgShape(string Name, IImmutableDictionary<string, string> Attributes);
}
