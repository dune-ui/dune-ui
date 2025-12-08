namespace StellarAdmin.TagHelpers;

public enum SidebarMenuSubLinkSize
{
    Small,
    Medium,
}

public static class SidebarMenuSubLinkSizeExtensions
{
    extension(SidebarMenuSubLinkSize size)
    {
        public string GetDataAttributeText()
        {
            return size switch
            {
                SidebarMenuSubLinkSize.Small => "sm",
                SidebarMenuSubLinkSize.Medium => "md",
                _ => string.Empty,
            };
        }
    }
}