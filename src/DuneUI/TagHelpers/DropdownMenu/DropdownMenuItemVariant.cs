namespace DuneUI.TagHelpers;

/// <summary>
///     The visual style of a dropdown menu item.
/// </summary>
public enum DropdownMenuItemVariant
{
    /// <summary>The standard item style.</summary>
    Default,

    /// <summary>A destructive item style, used for actions such as delete.</summary>
    Destructive,
}

internal static class DropdownMenuItemVariantExtensions
{
    extension(DropdownMenuItemVariant variant)
    {
        public string GetDataAttributeText() =>
            variant switch
            {
                DropdownMenuItemVariant.Default => "default",
                DropdownMenuItemVariant.Destructive => "destructive",
                _ => string.Empty,
            };
    }
}
