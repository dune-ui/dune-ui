namespace DuneUI.TagHelpers;

public enum ToggleGroupOrientation
{
    Horizontal,
    Vertical,
}

public static class ToggleGroupOrientationExtensions
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
