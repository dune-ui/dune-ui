namespace DuneUI.TagHelpers;

/// <summary>
///     The orientation of a <c>&lt;dui-separator&gt;</c>.
/// </summary>
public enum SeparatorOrientation
{
    /// <summary>A horizontal separator that divides stacked content.</summary>
    Horizontal,

    /// <summary>A vertical separator that divides side-by-side content.</summary>
    Vertical,
}

internal static class SeparatorOrientationExtensions
{
    extension(SeparatorOrientation orientation)
    {
        public string GetDataAttributeText() =>
            orientation switch
            {
                SeparatorOrientation.Horizontal => "horizontal",
                SeparatorOrientation.Vertical => "vertical",
                _ => string.Empty,
            };
    }
}
