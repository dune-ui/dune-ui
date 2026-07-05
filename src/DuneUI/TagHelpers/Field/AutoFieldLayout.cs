namespace DuneUI.TagHelpers;

/// <summary>
///     How an automatically rendered field arranges its input relative to the label, description, and error.
/// </summary>
public enum AutoFieldLayout
{
    /// <summary>The label, input, and supporting text are stacked vertically.</summary>
    Vertical,

    /// <summary>The input is placed first, with the label and supporting text following it horizontally.</summary>
    HorizontalInputFirst,

    /// <summary>The input is placed last, with the label and supporting text preceding it horizontally.</summary>
    HorizontalInputLast,
}
