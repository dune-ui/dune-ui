using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-item-content")]
public class ItemContentTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public ItemContentTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
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
                "flex flex-1 flex-col gap-1 [&+[data-slot=item-content]]:flex-none",
                GetUserSpecifiedClass(output)
            )
        );
        output.Attributes.SetAttribute("data-slot", "item-content");

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
