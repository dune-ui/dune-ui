namespace DuneUI.TagHelpers;

public enum DropdownMenuItemVariant
{
    Default,
    Destructive,
}

public static class DropdownMenuItemVariantExtensions
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
