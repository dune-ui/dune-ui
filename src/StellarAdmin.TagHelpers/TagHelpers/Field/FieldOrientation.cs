namespace StellarAdmin.TagHelpers;

public enum FieldOrientation
{
    Vertical,
    Horizontal,
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