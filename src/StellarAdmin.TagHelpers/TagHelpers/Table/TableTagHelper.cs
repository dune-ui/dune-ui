using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-table")]
public class TableTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public TableTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "table-container");
        output.Attributes.SetAttribute("class", "relative w-full overflow-x-auto");

        var tableTagBuilder = new TagBuilder("table");
        tableTagBuilder.Attributes.Add("data-slot", "table");
        tableTagBuilder.Attributes.Add(
            "class",
            _classMerger.Merge("w-full caption-bottom text-sm", output.GetUserSuppliedClass())
        );
        tableTagBuilder.InnerHtml.AppendHtml(await output.GetChildContentAsync());

        output.Content.AppendHtml(tableTagBuilder);
    }
}
