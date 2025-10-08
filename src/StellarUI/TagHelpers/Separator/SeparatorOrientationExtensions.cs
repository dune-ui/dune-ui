namespace StellarUI.TagHelpers;

internal static class SeparatorOrientationExtensions
{
    public static string GetValue(this SeparatorOrientation orientation) =>
        orientation switch
        {
            SeparatorOrientation.Horizontal => "horizontal",
            SeparatorOrientation.Vertical => "vertical",
            _ => orientation.ToString(),
        };
}