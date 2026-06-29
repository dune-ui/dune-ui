namespace DuneUI.TagHelpers;

public enum ToggleSize
{
    Default,
    Small,
    Large,
}

public static class ToggleSizeExtensions
{
    extension(ToggleSize size)
    {
        public string GetDataAttributeText() =>
            size switch
            {
                ToggleSize.Default => "default",
                ToggleSize.Small => "sm",
                ToggleSize.Large => "lg",
                _ => string.Empty,
            };
    }
}
