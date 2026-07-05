namespace DuneUI.TagHelpers;

/// <summary>
///     The visual style of a <c>&lt;dui-tab-list&gt;</c>.
/// </summary>
public enum TabListVariant
{
    /// <summary>The default style, rendering the tab list on a muted background.</summary>
    Default,

    /// <summary>A minimal style that marks the active tab with an underline.</summary>
    Line,
}

public static class TabListVariantExtensions
{
    extension(TabListVariant variant)
    {
        public string GetDataAttributeText() =>
            variant switch
            {
                TabListVariant.Default => "default",
                TabListVariant.Line => "line",
                _ => "",
            };
    }
}
