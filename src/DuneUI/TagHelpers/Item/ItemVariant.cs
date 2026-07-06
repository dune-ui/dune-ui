namespace DuneUI.TagHelpers;

/// <summary>
///     The visual style of an item.
/// </summary>
public enum ItemVariant
{
    /// <summary>A plain item with no border or background.</summary>
    Default,

    /// <summary>An item with a bordered outline.</summary>
    Outline,

    /// <summary>An item with a muted background.</summary>
    Muted,
}

internal static class ItemVariantExtensions
{
    extension(ItemVariant variant)
    {
        public string GetDataAttributeText()
        {
            return variant switch
            {
                ItemVariant.Default => "default",
                ItemVariant.Outline => "outline",
                ItemVariant.Muted => "muted",
                _ => string.Empty,
            };
        }
    }
}
