namespace DuneUI.TagHelpers;

/// <summary>
///     Whether a toggle group allows one or many items to be selected.
/// </summary>
public enum ToggleGroupType
{
    /// <summary>Only one item can be selected at a time (radio-backed, scalar binding).</summary>
    Single,

    /// <summary>Any number of items can be selected (checkbox-backed, collection binding).</summary>
    Multiple,
}

internal static class ToggleGroupTypeExtensions
{
    extension(ToggleGroupType type)
    {
        public string GetDataAttributeText() =>
            type switch
            {
                ToggleGroupType.Single => "single",
                ToggleGroupType.Multiple => "multiple",
                _ => string.Empty,
            };
    }
}
