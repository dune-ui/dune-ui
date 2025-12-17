using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-button")]
public class ButtonTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public ButtonTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    /// <summary>
    ///     The size of the button.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="ButtonSize.Default" />
    /// </remarks>
    [HtmlAttributeName("size")]
    public ButtonSize? Size { get; set; }

    /// <summary>
    ///     The button variant.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="ButtonVariant.Default" />.
    /// </remarks>
    [HtmlAttributeName("variant")]
    public ButtonVariant? Variant { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveSize = Size ?? ButtonSize.Default;
        var effectiveVariant = Variant ?? ButtonVariant.Default;

        output.TagName = "button";
        output.TagMode = TagMode.StartTagAndEndTag;

        ButtonRenderingHelper.RenderAttributes(
            output,
            _classMerger,
            effectiveVariant,
            effectiveSize
        );
    }
}
