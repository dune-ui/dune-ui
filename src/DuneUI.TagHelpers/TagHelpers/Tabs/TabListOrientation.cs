namespace DuneUI.TagHelpers;

public enum TabListOrientation
{
    Horizontal,
    Vertical,
}

public static class TabListOrientationExtensions
{
    extension(TabListOrientation orientation)
    {
        public string GetDataAttributeText() => orientation switch
        {
            TabListOrientation.Horizontal => "horizontal",
            TabListOrientation.Vertical => "vertical",
            _ => string.Empty
        };
    }
}