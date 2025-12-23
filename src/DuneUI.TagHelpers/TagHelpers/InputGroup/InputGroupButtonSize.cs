namespace DuneUI.TagHelpers;

public enum InputGroupButtonSize
{
    ExtraSmall,
    Small,
    IconExtraSmall,
    IconSmall,
}

public static class InputGroupButtonSizeExtensions
{
    extension(InputGroupButtonSize size)
    {
        public string GetDataAttributeText()
        {
            return size switch
            {
                InputGroupButtonSize.ExtraSmall => "xs",
                InputGroupButtonSize.Small => "sm",
                InputGroupButtonSize.IconExtraSmall => "icon-xs",
                InputGroupButtonSize.IconSmall => "icon-sm",
                _ => string.Empty
            };
        }
    }
}