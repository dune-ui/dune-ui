using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-breadcrumb-page")]
public class BreadcrumbPageTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public BreadcrumbPageTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

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
            _classMerger.Merge("text-foreground font-normal", output.GetUserSuppliedClass())
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
