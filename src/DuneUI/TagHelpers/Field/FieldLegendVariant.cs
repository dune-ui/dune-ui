namespace DuneUI.TagHelpers;

public enum FieldLegendVariant
{
    Legend,
    Label,
}

public static class FieldLegendVariantExtensions
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