namespace DuneUI.TagHelpers;

public enum SidebarMenuLinkSize
{
    Default,
    Small,
    Large,
}

public static class SidebarMenuLinkSizeExtensions
{
    extension(SidebarMenuLinkSize size)
    {
        public string GetDataAttributeText()
        {
            return size switch
            {
                SidebarMenuLinkSize.Default => "default",
                SidebarMenuLinkSize.Small => "sm",
                SidebarMenuLinkSize.Large => "lg",
                _ => String.Empty,
            };
        }
    }
}
