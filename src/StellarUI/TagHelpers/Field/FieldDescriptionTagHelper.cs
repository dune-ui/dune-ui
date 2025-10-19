using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-field-description")]
public class FieldDescriptionTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "field-description");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "text-muted-foreground text-sm leading-normal font-normal group-has-[[data-orientation=horizontal]]/field:text-balance",
                "last:mt-0 nth-last-2:-mt-1 [[data-variant=legend]+&]:-mt-1.5",
                "[&>a:hover]:text-primary [&>a]:underline [&>a]:underline-offset-4",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
