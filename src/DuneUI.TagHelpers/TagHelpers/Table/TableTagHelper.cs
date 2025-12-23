using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-table")]
public class TableTagHelper : DuneUITagHelperBase
{
    public TableTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "table-container");
        output.Attributes.SetAttribute(
            "class",
            ThemeManager.GetComponentClass("dui-table-container")
        );

        var tableTagBuilder = new TagBuilder("table");
        tableTagBuilder.Attributes.Add("data-slot", "table");
        tableTagBuilder.Attributes.Add(
            "class",
            ClassMerger.Merge(new ComponentName("dui-table"), output.GetUserSuppliedClass())
        );
        tableTagBuilder.InnerHtml.AppendHtml(await output.GetChildContentAsync());

        output.Content.AppendHtml(tableTagBuilder);
    }
}
