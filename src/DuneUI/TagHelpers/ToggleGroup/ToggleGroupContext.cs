using System.Collections;

namespace DuneUI.TagHelpers;

/// <summary>
///     The resolved configuration a <c>dui-toggle-group</c> publishes for its
///     <c>dui-toggle-group-item</c> children to consume. The group determines every value here;
///     an item only contributes its own <c>value</c> (and any per-item variant/size override).
/// </summary>
internal sealed class ToggleGroupContext
{
    public required ToggleGroupType Type { get; init; }

    public required ToggleVariant Variant { get; init; }

    public required ToggleSize Size { get; init; }

    public required int Spacing { get; init; }

    /// <summary>
    ///     The shared form-field name applied to every item's input, or <c>null</c> when the group
    ///     is not model-bound.
    /// </summary>
    public required string? FieldName { get; init; }

    /// <summary>
    ///     The value(s) bound through the group's <c>asp-for</c> — a scalar for a single-select
    ///     group, a collection for a multiple-select group, or <c>null</c> when not model-bound.
    /// </summary>
    public required object? SelectedValue { get; init; }

    /// <summary>
    ///     Determines whether an item with the given value is selected, based on the resolved
    ///     <see cref="Type" /> and the bound <see cref="SelectedValue" />: a scalar comparison for a
    ///     single-select group, a membership test for a multiple-select group.
    /// </summary>
    public bool IsItemSelected(string itemValue)
    {
        if (SelectedValue == null)
        {
            return false;
        }

        if (Type == ToggleGroupType.Single)
        {
            return string.Equals(SelectedValue.ToString(), itemValue, StringComparison.Ordinal);
        }

        if (SelectedValue is IEnumerable enumerable and not string)
        {
            foreach (var entry in enumerable)
            {
                if (string.Equals(entry?.ToString(), itemValue, StringComparison.Ordinal))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
