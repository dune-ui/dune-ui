namespace DuneUI.TagHelpers;

public enum SidebarCollapsible
{
    /// <summary>
    ///     The sidebar slides off-canvas when collapsed.
    /// </summary>
    Offcanvas,

    /// <summary>
    ///     The sidebar collapses to a narrow icon rail when collapsed.
    /// </summary>
    Icon,

    /// <summary>
    ///     The sidebar cannot be collapsed.
    /// </summary>
    None,
}

public static class SidebarCollapsibleExtensions
{
    extension(SidebarCollapsible collapsible)
    {
        public string GetDataAttributeText() =>
            collapsible switch
            {
                SidebarCollapsible.Offcanvas => "offcanvas",
                SidebarCollapsible.Icon => "icon",
                SidebarCollapsible.None => "none",
                _ => string.Empty
            };
    }
}
