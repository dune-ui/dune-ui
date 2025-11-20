using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-breadcrumb-page")]
public class BreadcrumbPageTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "breadcrumb-page");
        output.Attributes.SetAttribute("role", "link");
        output.Attributes.SetAttribute("aria-disabled", "true");
        output.Attributes.SetAttribute("aria-current", "page");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge("text-foreground font-normal", output.GetUserSuppliedClass())
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
