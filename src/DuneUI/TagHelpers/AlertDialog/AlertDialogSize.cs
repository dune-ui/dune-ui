namespace DuneUI.TagHelpers;

/// <summary>
///     The size of an alert dialog.
/// </summary>
public enum AlertDialogSize
{
    /// <summary>The standard alert dialog size.</summary>
    Default,

    /// <summary>A compact alert dialog size.</summary>
    Small,
}

internal static class AlertDialogSizeExtensions
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
