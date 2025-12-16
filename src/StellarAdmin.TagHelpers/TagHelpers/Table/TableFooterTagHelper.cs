using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-table-footer")]
public class TableFooterTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public TableFooterTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "tfoot";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "table-footer");
        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge(
                "bg-muted/50 border-t font-medium [&>tr]:last:border-b-0",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
