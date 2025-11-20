using System.Collections.Immutable;

namespace StellarAdmin.Icons;

public record SvgShape(string Name, IImmutableDictionary<string, string> Attributes);
