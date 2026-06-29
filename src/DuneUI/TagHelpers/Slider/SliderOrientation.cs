namespace DuneUI.TagHelpers;

public enum SliderOrientation
{
    Horizontal,
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
