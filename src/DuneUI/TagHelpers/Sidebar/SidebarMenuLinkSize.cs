namespace DuneUI.TagHelpers;

/// <summary>
///     The size of a sidebar menu link.
/// </summary>
public enum SidebarMenuLinkSize
{
    /// <summary>The standard link height.</summary>
    Default,

    /// <summary>A more compact link.</summary>
    Small,

    /// <summary>A taller link, useful for prominent entries.</summary>
    Large,
}

internal static class SidebarMenuLinkSizeExtensions
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
