using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-breadcrumb")]
public class BreadcrumbTagHelper : StellarTagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "nav";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("aria-label", "breadcrumb");
        output.Attributes.SetAttribute("data-slot", "breadcrumb");

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
