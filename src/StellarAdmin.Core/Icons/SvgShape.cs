using System.Collections.Immutable;

namespace StellarUI.Icons;

public record SvgShape(string Name, IImmutableDictionary<string, string> Attributes);