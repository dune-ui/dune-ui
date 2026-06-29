namespace DuneUI.TagHelpers;

public enum ToggleVariant
{
    Default,
    Outline,
}

public static class ToggleVariantExtensions
{
    extension(ToggleVariant variant)
    {
        public string GetDataAttributeText() =>
            variant switch
            {
                ToggleVariant.Default => "default",
                ToggleVariant.Outline => "outline",
                _ => string.Empty,
            };
    }
}
