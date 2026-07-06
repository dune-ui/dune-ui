namespace DuneUI.TagHelpers;

/// <summary>
///     The visual style of a sidebar.
/// </summary>
public enum SidebarVariant
{
    /// <summary>A standard sidebar flush against the edge of the screen.</summary>
    Sidebar,

    /// <summary>A sidebar that floats with a border and rounded corners.</summary>
    Floating,

    /// <summary>A sidebar that insets the main content area within it.</summary>
    Inset,
}

internal static class SidebarVariantExtensions
{
    extension(SidebarVariant variant)
    {
        public string GetDataAttributeText() =>
            variant switch
            {
                SidebarVariant.Sidebar => "sidebar",
                SidebarVariant.Floating => "floating",
                SidebarVariant.Inset => "inset",
                _ => string.Empty,
            };
    }
}
