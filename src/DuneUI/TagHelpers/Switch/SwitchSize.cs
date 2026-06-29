namespace DuneUI.TagHelpers;

public enum SwitchSize
{
    Default,
    Small,
}

public static class SwitchSizeExtensions
{
    extension(SwitchSize size)
    {
        public string GetDataAttributeText() =>
            size switch
            {
                SwitchSize.Default => "default",
                SwitchSize.Small => "sm",
                _ => string.Empty,
            };
    }
}
