using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-alert-description")]
public class AlertDescriptionTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "alert-description");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "text-muted-foreground col-start-2 grid justify-items-start gap-1 text-sm [&_p]:leading-relaxed",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.SetHtmlContent(await output.GetChildContentAsync());
    }
}
