namespace DuneUI.TagHelpers;

/// <summary>
///     The size of a sidebar submenu button or link.
/// </summary>
public enum SidebarMenuSubLinkSize
{
    /// <summary>A more compact size.</summary>
    Small,

    /// <summary>The standard size.</summary>
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
