using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-item-header")]
public class ItemHeaderTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public ItemHeaderTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
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
            _classMerger.Merge(
                "flex basis-full items-center justify-between gap-2",
                GetUserSpecifiedClass(output)
            )
        );
        output.Attributes.SetAttribute("data-slot", "item-header");

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
