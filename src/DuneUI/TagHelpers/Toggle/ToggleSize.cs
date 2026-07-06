namespace DuneUI.TagHelpers;

/// <summary>
///     The size of a toggle.
/// </summary>
public enum ToggleSize
{
    /// <summary>The default toggle size.</summary>
    Default,

    /// <summary>A small toggle.</summary>
    Small,

    /// <summary>A large toggle.</summary>
    Large,
}

internal static class ToggleSizeExtensions
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
