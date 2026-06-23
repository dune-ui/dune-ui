namespace DuneUI.TagHelpers;

public enum SidebarSide
{
    Left,
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
                _ => string.Empty
            };
    }
}
