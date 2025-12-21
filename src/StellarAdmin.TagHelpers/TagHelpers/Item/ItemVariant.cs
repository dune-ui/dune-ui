namespace StellarAdmin.TagHelpers;

public enum ItemVariant
{
    Default,
    Outline,
    Muted,
}

public static class ItemVariantExtensions
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