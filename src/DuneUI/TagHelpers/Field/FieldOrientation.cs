namespace DuneUI.TagHelpers;

/// <summary>
///     How a field arranges its label, control, and supporting text.
/// </summary>
public enum FieldOrientation
{
    /// <summary>The label, control, and supporting text are stacked vertically.</summary>
    Vertical,

    /// <summary>The label, control, and supporting text are laid out horizontally.</summary>
    Horizontal,

    /// <summary>Stacks vertically on narrow containers and switches to horizontal on wider ones.</summary>
    Responsive,
}

public static class FieldOrientationExtensions
{
    extension(FieldOrientation orientation)
    {
        public string GetDataAttributeText() =>
            orientation switch
            {
                FieldOrientation.Vertical => "vertical",
                FieldOrientation.Horizontal => "horizontal",
                FieldOrientation.Responsive => "responsive",
                _ => string.Empty,
            };
    }
}
