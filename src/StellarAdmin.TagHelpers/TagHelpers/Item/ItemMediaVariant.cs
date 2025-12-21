namespace StellarAdmin.TagHelpers;

public enum ItemMediaVariant
{
    Default,
    Icon,
    Image,
}

public static class ItemMediaVariantExtensions
{
    extension(ItemMediaVariant variant)
    {
        public string GetDataAttributeText() => variant switch
        {
            ItemMediaVariant.Default => "default",
            ItemMediaVariant.Icon => "icon",
            ItemMediaVariant.Image => "image",
            _ => ""
        };
    }
}