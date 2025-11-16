namespace StellarUI.TagHelpers;

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
                SidebarMenuLinkSize.Small => "small",
                SidebarMenuLinkSize.Large => "large",
                _ => String.Empty,
            };
        }
    }
}
