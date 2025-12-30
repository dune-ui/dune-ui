namespace DuneUI.TagHelpers;

[Flags]
public enum AutoFieldElement
{
    None = 0,
    Label = 1 << 0,
    Description = 1 << 1,
    Error = 1 << 2,
    All = Label | Description | Error,
}

public static class AutoFieldElementExtensions
{
    public static bool HasFlagFast(this AutoFieldElement value, AutoFieldElement flag)
    {
        return (value & flag) != 0;
    }
}
