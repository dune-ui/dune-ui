using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-breadcrumb")]
public class BreadcrumbTagHelper : StellarTagHelper
{
    public BreadcrumbTagHelper(ThemeManager themeManager)
        : base(themeManager) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "nav";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("aria-label", "breadcrumb");
        output.Attributes.SetAttribute("data-slot", "breadcrumb");

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
