namespace DuneUI.TagHelpers;

/// <summary>
///     The edge of the screen a sidebar is anchored to.
/// </summary>
public enum SidebarSide
{
    /// <summary>Anchored to the left edge.</summary>
    Left,

    /// <summary>Anchored to the right edge.</summary>
    Right,
}

public static class SidebarSideExtensions
{
    extension(SidebarSide side)
    {
        public string GetDataAttributeText() =>
            side switch
            {
                SidebarSide.Left => "left",
                SidebarSide.Right => "right",
                _ => string.Empty,
            };
    }
}
