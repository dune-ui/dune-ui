namespace DuneUI.TagHelpers;

/// <summary>
///     The size of a <c>&lt;dui-switch&gt;</c>.
/// </summary>
public enum SwitchSize
{
    /// <summary>The default switch size.</summary>
    Default,

    /// <summary>A smaller, more compact switch.</summary>
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
