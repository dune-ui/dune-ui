namespace StellarAdmin.TagHelpers;

public enum SeparatorOrientation
{
    Horizontal,
    Vertical,
}

public static class SeparatorOrientationExtensions
{
    extension(SeparatorOrientation orientation)
    {
        public string GetDataAttributeText() =>
            orientation switch
            {
                SeparatorOrientation.Horizontal => "horizontal",
                SeparatorOrientation.Vertical => "vertical",
                _ => string.Empty
            };
    }
}