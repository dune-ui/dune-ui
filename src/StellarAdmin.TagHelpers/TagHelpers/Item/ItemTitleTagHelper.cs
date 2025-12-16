using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-item-title")]
public class ItemTitleTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public ItemTitleTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
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
                "flex w-fit items-center gap-2 text-sm leading-snug font-medium",
                GetUserSpecifiedClass(output)
            )
        );
        output.Attributes.SetAttribute("data-slot", "item-title");

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
