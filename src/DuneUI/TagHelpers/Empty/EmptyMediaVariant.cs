namespace DuneUI.TagHelpers;

/// <summary>
///     The visual style of a <c>&lt;dui-empty-media&gt;</c> region.
/// </summary>
public enum EmptyMediaVariant
{
    /// <summary>The default media style, for illustrations or images.</summary>
    Default,

    /// <summary>A style suited to a single icon, rendered within a muted container.</summary>
    Icon,
}

internal static class EmptyMediaVariantExtensions
{
    extension(EmptyMediaVariant variant)
    {
        public string GetDataAttributeText()
        {
            return variant switch
            {
                EmptyMediaVariant.Default => "default",
                EmptyMediaVariant.Icon => "icon",
                _ => string.Empty,
            };
        }
    }
}
