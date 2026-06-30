namespace DuneUI.TagHelpers;

public enum SliderThumbAlignment
{
    Center,
    Edge,
}

public static class SliderThumbAlignmentExtensions
{
    extension(SliderThumbAlignment alignment)
    {
        public string GetDataAttributeText() =>
            alignment switch
            {
                SliderThumbAlignment.Center => "center",
                SliderThumbAlignment.Edge => "edge",
                _ => string.Empty,
            };
    }
}
