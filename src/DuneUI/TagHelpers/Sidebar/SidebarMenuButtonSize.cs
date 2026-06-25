namespace DuneUI.TagHelpers;

public enum SidebarMenuButtonSize
{
    Default,
    Small,
    Large,
}

public static class SidebarMenuButtonSizeExtensions
{
    extension(SidebarMenuButtonSize size)
    {
        public string GetDataAttributeText()
        {
            return size switch
            {
                SidebarMenuButtonSize.Default => "default",
                SidebarMenuButtonSize.Small => "sm",
                SidebarMenuButtonSize.Large => "lg",
                _ => String.Empty,
            };
        }
    }
}
