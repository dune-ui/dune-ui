using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-breadcrumb-page")]
public class BreadcrumbPageTagHelper : StellarTagHelper
{
    public BreadcrumbPageTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "breadcrumb-page");
        output.Attributes.SetAttribute("role", "link");
        output.Attributes.SetAttribute("aria-disabled", "true");
        output.Attributes.SetAttribute("aria-current", "page");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ComponentName("dui-breadcrumb-page"),
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
