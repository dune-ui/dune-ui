using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-breadcrumb-link")]
public class BreadcrumbLinkTagHelper(IHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
    : StellarAnchorTagHelperBase(htmlGenerator)
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "a";
        output.TagMode = TagMode.StartTagAndEndTag;

        ApplyAnchorAttributes(output);

        output.Attributes.SetAttribute("data-slot", "breadcrumb-link");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "hover:text-foreground transition-colors",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
