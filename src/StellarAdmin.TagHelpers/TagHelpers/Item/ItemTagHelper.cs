using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-item")]
public class ItemTagHelper : StellarTagHelper
{
    [HtmlAttributeName("size")]
    public ItemSize? Size { get; set; }

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
