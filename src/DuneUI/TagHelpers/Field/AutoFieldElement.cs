namespace DuneUI.TagHelpers;

/// <summary>
///     The supporting elements that a field can automatically render around its input.
/// </summary>
[Flags]
public enum AutoFieldElement
{
    /// <summary>No supporting elements are rendered.</summary>
    None = 0,

    /// <summary>The field's label is rendered.</summary>
    Label = 1 << 0,

    /// <summary>The field's description text is rendered.</summary>
    Description = 1 << 1,

    /// <summary>The field's validation error message is rendered.</summary>
    Error = 1 << 2,

    /// <summary>The label, description, and error are all rendered.</summary>
    All = Label | Description | Error,
}

internal static class AutoFieldElementExtensions
{
    public static bool HasFlagFast(this AutoFieldElement value, AutoFieldElement flag)
    {
        return (value & flag) != 0;
    }
}
