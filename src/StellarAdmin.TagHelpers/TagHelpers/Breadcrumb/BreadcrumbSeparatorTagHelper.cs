using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarUI.Icons;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-breadcrumb-separator")]
public class BreadcrumbSeparatorTagHelper(ICssClassMerger classMerger, IIconManager iconManager)
    : StellarTagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "li";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "breadcrumb-separator");
        output.Attributes.SetAttribute("role", "presentation");
        output.Attributes.SetAttribute("aria-hidden", "true");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge("[&>svg]:size-3.5", output.GetUserSuppliedClass())
        );

        var childContent = await output.GetChildContentAsync();
        if (!childContent.IsEmptyOrWhiteSpace)
        {
            output.Content.AppendHtml(await output.GetChildContentAsync());
        }
        else
        {
            /* Render the chevron-right icon */
            var iconOutput = new TagHelperOutput(
                "svg",
                [new TagHelperAttribute("class", "size-4")],
                (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
            );
            var iconTagHelper = new IconTagHelper(iconManager) { Name = "chevron-right" };
            await iconTagHelper.ProcessAsync(context, iconOutput);
            output.Content.AppendHtml(iconOutput);
        }
    }
}
