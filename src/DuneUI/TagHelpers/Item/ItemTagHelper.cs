using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A flexible row for presenting content, combining media, a title, description, and actions.
/// </summary>
[HtmlTargetElement("dui-item")]
public class ItemTagHelper : DuneUITagHelperBase
{
    /// <summary>
    ///     The size of the item, controlling its padding and spacing.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="ItemSize.Default" />.
    /// </remarks>
    [HtmlAttributeName("size")]
    public ItemSize? Size { get; set; }

    /// <summary>
    ///     The visual style of the item.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="ItemVariant.Default" />.
    /// </remarks>
    [HtmlAttributeName("variant")]
    public ItemVariant? Variant { get; set; }

    public ItemTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        await ItemRenderingHelper.RenderAsync(output, ClassMerger, Size, Variant);
    }
}
