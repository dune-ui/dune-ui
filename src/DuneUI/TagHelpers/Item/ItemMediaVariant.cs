namespace DuneUI.TagHelpers;

/// <summary>
///     The kind of media held by an item's media region.
/// </summary>
public enum ItemMediaVariant
{
    /// <summary>Unstyled media with no special sizing.</summary>
    Default,

    /// <summary>A small icon.</summary>
    Icon,

    /// <summary>A full-bleed image or avatar.</summary>
    Image,
}

internal static class ItemMediaVariantExtensions
{
    extension(ItemMediaVariant variant)
    {
        public string GetDataAttributeText() =>
            variant switch
            {
                ItemMediaVariant.Default => "default",
                ItemMediaVariant.Icon => "icon",
                ItemMediaVariant.Image => "image",
                _ => "",
            };
    }
}
