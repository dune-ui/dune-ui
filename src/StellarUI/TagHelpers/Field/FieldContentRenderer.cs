using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

internal class FieldContentRenderer(ICssClassMerger classMerger)
{
    public async Task Render(TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "field-content");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "group/field-content flex flex-1 flex-col gap-1.5 leading-snug",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}