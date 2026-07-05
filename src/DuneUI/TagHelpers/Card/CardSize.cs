namespace DuneUI.TagHelpers;

/// <summary>
///     The size of a <c>&lt;dui-card&gt;</c>, controlling its padding and spacing.
/// </summary>
public enum CardSize
{
    /// <summary>The default card size.</summary>
    Default,

    /// <summary>A compact card with reduced padding and spacing.</summary>
    Small,
}

public static class CardSizeExtensions
{
    extension(CardSize size)
    {
        public string GetDataAttributeText() =>
            size switch
            {
                CardSize.Default => "default",
                CardSize.Small => "sm",
                _ => string.Empty,
            };
    }
}
