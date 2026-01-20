namespace DuneUI.TagHelpers;

public enum InputGroupAddOnVariantAlignment
{
    InlineStart,
    InlineEnd,
    BlockStart,
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
                _ => string.Empty
            };
        }
    }
}
