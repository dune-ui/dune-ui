namespace DuneUI.TagHelpers;

/// <summary>
///     The visual style applied to a field set's legend.
/// </summary>
public enum FieldLegendVariant
{
    /// <summary>Styled as a standard legend.</summary>
    Legend,

    /// <summary>Styled to match a field label.</summary>
    Label,
}

internal static class FieldLegendVariantExtensions
{
    extension(FieldLegendVariant variant)
    {
        public string GetDataAttributeText() =>
            variant switch
            {
                FieldLegendVariant.Legend => "legend",
                FieldLegendVariant.Label => "label",
                _ => string.Empty,
            };
    }
}
