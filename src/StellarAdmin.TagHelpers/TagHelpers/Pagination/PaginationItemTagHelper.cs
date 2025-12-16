using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-pagination-item")]
public class PaginationItemTagHelper : StellarTagHelper
{
    public PaginationItemTagHelper(ThemeManager themeManager)
        : base(themeManager) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "li";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "pagination-item");

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
