using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-breadcrumb")]
public class BreadcrumbTagHelper : StellarTagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "nav";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.Add("aria-label", "breadcrumb");
        output.Attributes.Add("data-slot", "breadcrumb");

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
