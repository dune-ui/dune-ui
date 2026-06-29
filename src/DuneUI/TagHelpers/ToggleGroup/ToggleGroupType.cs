namespace DuneUI.TagHelpers;

public enum ToggleGroupType
{
    Single,
    Multiple,
}

public static class ToggleGroupTypeExtensions
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
