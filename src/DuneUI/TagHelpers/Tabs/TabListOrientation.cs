namespace DuneUI.TagHelpers;

/// <summary>
///     The orientation in which a <c>&lt;dui-tab-list&gt;</c> arranges its tabs.
/// </summary>
public enum TabListOrientation
{
    /// <summary>The tabs are arranged horizontally.</summary>
    Horizontal,

    /// <summary>The tabs are arranged vertically.</summary>
    Vertical,
}

internal static class TabListOrientationExtensions
{
    extension(TabListOrientation orientation)
    {
        public string GetDataAttributeText() =>
            orientation switch
            {
                TabListOrientation.Horizontal => "horizontal",
                TabListOrientation.Vertical => "vertical",
                _ => string.Empty,
            };
    }
}
