namespace DuneUI.TagHelpers;

/// <summary>
///     Controls how a <c>&lt;dui-group&gt;</c> aligns its children along the cross axis.
/// </summary>
public enum GroupAlign
{
    /// <summary>Children stretch to fill the cross axis.</summary>
    Stretch,

    /// <summary>Children are centered on the cross axis.</summary>
    Center,

    /// <summary>Children are aligned to the start of the cross axis.</summary>
    Start,

    /// <summary>Children are aligned to the end of the cross axis.</summary>
    End,

    /// <summary>Children are aligned along their text baselines.</summary>
    Baseline,
}
