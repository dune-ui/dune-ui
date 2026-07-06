namespace DuneUI.TagHelpers;

/// <summary>
///     The size of an item, controlling its padding and spacing.
/// </summary>
public enum ItemSize
{
    /// <summary>The standard item size.</summary>
    Default,

    /// <summary>A compact item size.</summary>
    Small,

    /// <summary>The most compact item size.</summary>
    ExtraSmall,
}

internal static class ItemSizeExtensions
{
    extension(ItemSize size)
    {
        public string GetDataAttributeText()
        {
            return size switch
            {
                ItemSize.Default => "default",
                ItemSize.Small => "sm",
                ItemSize.ExtraSmall => "xs",
                _ => string.Empty,
            };
        }
    }
}
