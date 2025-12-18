namespace StellarAdmin.TagHelpers;

public enum ButtonGroupOrientation
{
    Horizontal,
    Vertical,
}

public static class ButtonGroupOrientationExtensions
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
