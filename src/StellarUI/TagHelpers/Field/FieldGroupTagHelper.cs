using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-field-group")]
public class FieldGroupTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "field-group");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "group/field-group @container/field-group flex w-full flex-col gap-7 data-[slot=checkbox-group]:gap-3 [&>[data-slot=field-group]]:gap-4",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
