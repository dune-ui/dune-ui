namespace DuneUI.TagHelpers;

public enum TabListVariant
{
    Default,
    Line
}

public static class TabListVariantExtensions
{
    extension(TabListVariant variant)
    {
        public string GetDataAttributeText() => variant switch
        {
            TabListVariant.Default => "default",
            TabListVariant.Line => "line",
            _ => ""
        };
    }
}