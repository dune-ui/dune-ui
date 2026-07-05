namespace DuneUI.TagHelpers;

/// <summary>
///     The visual style of a button.
/// </summary>
public enum ButtonVariant
{
    /// <summary>The default, primary button style.</summary>
    Default,

    /// <summary>A style that signals a destructive action.</summary>
    Destructive,

    /// <summary>A button with a border and transparent background.</summary>
    Outline,

    /// <summary>A secondary, lower-emphasis button style.</summary>
    Secondary,

    /// <summary>A button with no background or border until interacted with.</summary>
    Ghost,

    /// <summary>A button styled as a hyperlink.</summary>
    Link,
}
