namespace DuneUI.TagHelpers;

/// <summary>
///     The size of a button rendered inside an input group.
/// </summary>
public enum InputGroupButtonSize
{
    /// <summary>An extra-small button.</summary>
    ExtraSmall,

    /// <summary>A small button.</summary>
    Small,

    /// <summary>An extra-small square button sized for a single icon.</summary>
    IconExtraSmall,

    /// <summary>A small square button sized for a single icon.</summary>
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
                _ => string.Empty,
            };
        }
    }
}
