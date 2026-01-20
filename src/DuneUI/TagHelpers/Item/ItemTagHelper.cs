using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-item")]
public class ItemTagHelper : DuneUITagHelperBase
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
