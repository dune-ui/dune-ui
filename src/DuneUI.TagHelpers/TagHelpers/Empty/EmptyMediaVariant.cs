namespace DuneUI.TagHelpers;

public enum EmptyMediaVariant
{
    Default,
    Icon,
}

public static class EmptyMediaVariantExtensions
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