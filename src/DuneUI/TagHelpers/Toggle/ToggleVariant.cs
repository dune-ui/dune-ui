namespace DuneUI.TagHelpers;

/// <summary>
///     The visual style of a toggle.
/// </summary>
public enum ToggleVariant
{
    /// <summary>The default toggle style with no border.</summary>
    Default,

    /// <summary>A toggle with a border.</summary>
    Outline,
}

public static class ToggleVariantExtensions
{
    extension(ToggleVariant variant)
    {
        public string GetDataAttributeText() =>
            variant switch
            {
                ToggleVariant.Default => "default",
                ToggleVariant.Outline => "outline",
                _ => string.Empty,
            };
    }
}
