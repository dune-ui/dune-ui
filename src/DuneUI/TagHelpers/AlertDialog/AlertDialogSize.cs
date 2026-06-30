namespace DuneUI.TagHelpers;

public enum AlertDialogSize
{
    Default,
    Small,
}

public static class AlertDialogSizeExtensions
{
    extension(AlertDialogSize size)
    {
        public string GetDataAttributeText() =>
            size switch
            {
                AlertDialogSize.Default => "default",
                AlertDialogSize.Small => "sm",
                _ => string.Empty,
            };
    }
}
