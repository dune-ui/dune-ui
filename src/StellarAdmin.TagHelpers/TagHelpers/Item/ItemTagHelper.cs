using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-item")]
public class ItemTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public ItemTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    [HtmlAttributeName("size")]
    public ItemSize Size { get; set; } = ItemSize.Default;

    [HtmlAttributeName("variant")]
    public ItemVariant Variant { get; set; } = ItemVariant.Default;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        var itemRenderer = new ItemRenderer(_classMerger);
        await itemRenderer.RenderAsync(context, output, Size, Variant);
    }
}
