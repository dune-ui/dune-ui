namespace StellarAdmin.TagHelpers;

public enum ItemSize
{
    Default,
    Small,
    ExtraSmall
}

public static class ItemSizeExtensions
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