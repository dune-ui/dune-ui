namespace StellarAdmin.TagHelpers;

public enum CardSize
{
    Default,
    Small,
}

public static class CardSizeExtensions
{
    extension(CardSize size)
    {
        public string GetDataAttributeText() => size switch
        {
            CardSize.Default => "default",
            CardSize.Small => "sm",
            _ => string.Empty
        };
    }
}