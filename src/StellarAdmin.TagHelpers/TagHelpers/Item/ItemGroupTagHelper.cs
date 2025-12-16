using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-item-group")]
public class ItemGroupTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public ItemGroupTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge("group/item-group flex flex-col", GetUserSpecifiedClass(output))
        );
        output.Attributes.SetAttribute("data-slot", "item-group");

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
