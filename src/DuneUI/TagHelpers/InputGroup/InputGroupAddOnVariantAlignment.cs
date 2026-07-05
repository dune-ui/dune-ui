namespace DuneUI.TagHelpers;

/// <summary>
///     Where an input group add-on is positioned relative to the input.
/// </summary>
public enum InputGroupAddOnVariantAlignment
{
    /// <summary>Aligned to the leading (inline start) edge of the input.</summary>
    InlineStart,

    /// <summary>Aligned to the trailing (inline end) edge of the input.</summary>
    InlineEnd,

    /// <summary>Placed on its own row above the input.</summary>
    BlockStart,

    /// <summary>Placed on its own row below the input.</summary>
    BlockEnd,
}

internal static class InputGroupAddOnVariantAlignmentExtensions
{
    extension(InputGroupAddOnVariantAlignment alignment)
    {
        public string GetDataAttributeText()
        {
            return alignment switch
            {
                InputGroupAddOnVariantAlignment.InlineStart => "inline-start",
                InputGroupAddOnVariantAlignment.InlineEnd => "inline-end",
                InputGroupAddOnVariantAlignment.BlockStart => "block-start",
                InputGroupAddOnVariantAlignment.BlockEnd => "block-end",
                _ => string.Empty,
            };
        }
    }
}
