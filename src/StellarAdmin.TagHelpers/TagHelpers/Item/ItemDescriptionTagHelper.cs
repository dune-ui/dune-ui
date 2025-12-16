using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-item-description")]
public class ItemDescriptionTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public ItemDescriptionTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
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
                "text-muted-foreground line-clamp-2 text-sm leading-normal font-normal text-balance",
                "[&>a:hover]:text-primary [&>a]:underline [&>a]:underline-offset-4",
                GetUserSpecifiedClass(output)
            )
        );
        output.Attributes.SetAttribute("data-slot", "item-description");

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
