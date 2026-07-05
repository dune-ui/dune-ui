namespace DuneUI.TagHelpers;

/// <summary>
///     The orientation in which a <c>&lt;dui-slider&gt;</c> is laid out.
/// </summary>
public enum SliderOrientation
{
    /// <summary>The slider is laid out horizontally.</summary>
    Horizontal,

    /// <summary>The slider is laid out vertically.</summary>
    Vertical,
}

public static class SliderOrientationExtensions
{
    extension(SliderOrientation orientation)
    {
        public string GetDataAttributeText() =>
            orientation switch
            {
                SliderOrientation.Horizontal => "horizontal",
                SliderOrientation.Vertical => "vertical",
                _ => string.Empty,
            };
    }
}
