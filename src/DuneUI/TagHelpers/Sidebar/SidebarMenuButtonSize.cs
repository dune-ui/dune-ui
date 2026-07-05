namespace DuneUI.TagHelpers;

/// <summary>
///     The size of a sidebar menu button.
/// </summary>
public enum SidebarMenuButtonSize
{
    /// <summary>The standard button height.</summary>
    Default,

    /// <summary>A more compact button.</summary>
    Small,

    /// <summary>A taller button, useful for prominent entries.</summary>
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
