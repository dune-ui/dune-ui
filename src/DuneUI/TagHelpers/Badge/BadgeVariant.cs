namespace DuneUI.TagHelpers;

/// <summary>
///     The visual style of a <c>&lt;dui-badge&gt;</c>.
/// </summary>
public enum BadgeVariant
{
    /// <summary>The default, solid badge style.</summary>
    Default,

    /// <summary>A muted, secondary badge style.</summary>
    Secondary,

    /// <summary>A badge styled to indicate a destructive or error state.</summary>
    Destructive,

    /// <summary>A badge with a transparent background and a visible border.</summary>
    Outline,

    /// <summary>A badge with no background or border until interacted with.</summary>
    Ghost,

    /// <summary>A badge styled to look like a hyperlink.</summary>
    Link,
}
