namespace DuneUI.TagHelpers;

public enum SelectSize
{
    Default,
    Small,
}

public static class SelectSizeExtensions
{
    extension(SelectSize size)
    {
        public string GetDataAttributeText() => size switch
        {
            SelectSize.Default => "default",
            SelectSize.Small => "sm",
            _ => ""
        };
    }
}