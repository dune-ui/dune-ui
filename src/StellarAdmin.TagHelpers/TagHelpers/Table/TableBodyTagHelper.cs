using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-table-body")]
public class TableBodyTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public TableBodyTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "tbody";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "table-body");
        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge("[&_tr:last-child]:border-0", output.GetUserSuppliedClass())
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
