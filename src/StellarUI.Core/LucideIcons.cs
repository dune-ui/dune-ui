using System.Collections.Immutable;

namespace StellarUI;

public static partial class LucideIcons
{
    public record SvgShape(string Name, IImmutableDictionary<string, string> Attributes);
}
