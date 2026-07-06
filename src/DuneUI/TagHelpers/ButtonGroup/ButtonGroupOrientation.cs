namespace DuneUI.TagHelpers;

/// <summary>
///     The direction in which a button group lays out its items.
/// </summary>
public enum ButtonGroupOrientation
{
    /// <summary>Items are arranged in a row.</summary>
    Horizontal,

    /// <summary>Items are stacked in a column.</summary>
    Vertical,
}

internal static class ButtonGroupOrientationExtensions
{
    extension(ButtonGroupOrientation orientation)
    {
        public string GetDataAttributeText()
        {
            return orientation switch
            {
                ButtonGroupOrientation.Horizontal => "horizontal",
                ButtonGroupOrientation.Vertical => "vertical",
                _ => string.Empty,
            };
        }
    }
}
