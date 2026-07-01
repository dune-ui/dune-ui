namespace DuneUI.TagHelpers;

/// <summary>
///     The configuration a <c>dui-dropdown-menu-radio-group</c> publishes for its
///     <c>dui-dropdown-menu-radio-item</c> children. The group owns both values here: the generated
///     group name (so the web component can enforce single-selection) and the selected value (so each
///     item can render its initial checked state).
/// </summary>
internal sealed class DropdownMenuRadioGroupContext
{
    /// <summary>
    ///     The generated name shared by every item in the group, emitted as <c>data-radio-group</c>.
    /// </summary>
    public required string GroupName { get; init; }

    /// <summary>
    ///     The group's selected value, or <c>null</c> when nothing is selected.
    /// </summary>
    public required string? SelectedValue { get; init; }

    /// <summary>
    ///     Whether the item with the given value is the selected one.
    /// </summary>
    public bool IsSelected(string? itemValue) =>
        itemValue != null && string.Equals(SelectedValue, itemValue, StringComparison.Ordinal);
}
