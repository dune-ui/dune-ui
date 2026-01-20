using System.Collections.Immutable;

namespace DuneUI.Icons;

public record SvgShape(string Name, IImmutableDictionary<string, string> Attributes);
