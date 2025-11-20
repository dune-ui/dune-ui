using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sa-table")]
public class TableTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
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
            classMerger.Merge("w-full caption-bottom text-sm", output.GetUserSuppliedClass())
        );
        tableTagBuilder.InnerHtml.AppendHtml(await output.GetChildContentAsync());

        output.Content.AppendHtml(tableTagBuilder);
    }
}
