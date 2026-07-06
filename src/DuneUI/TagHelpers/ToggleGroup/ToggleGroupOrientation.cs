namespace DuneUI.TagHelpers;

/// <summary>
///     The direction in which a toggle group lays out its items.
/// </summary>
public enum ToggleGroupOrientation
{
    /// <summary>Items are arranged in a row.</summary>
    Horizontal,

    /// <summary>Items are stacked in a column.</summary>
    Vertical,
}

internal static class ToggleGroupOrientationExtensions
{
    extension(ToggleGroupOrientation orientation)
    {
        public string GetDataAttributeText() =>
            orientation switch
            {
                ToggleGroupOrientation.Horizontal => "horizontal",
                ToggleGroupOrientation.Vertical => "vertical",
                _ => string.Empty,
            };
    }
}
