namespace DuneUI.TagHelpers;

public enum SidebarVariant
{
    Sidebar,
    Floating,
    Inset,
}

public static class SidebarVariantExtensions
{
    extension(SidebarVariant variant)
    {
        public string GetDataAttributeText() =>
            variant switch
            {
                SidebarVariant.Sidebar => "sidebar",
                SidebarVariant.Floating => "floating",
                SidebarVariant.Inset => "inset",
                _ => string.Empty
            };
    }
}
