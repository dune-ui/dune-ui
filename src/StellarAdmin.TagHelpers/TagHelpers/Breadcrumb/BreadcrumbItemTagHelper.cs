using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-breadcrumb-item")]
public class BreadcrumbItemTagHelper : StellarTagHelper
{
    public BreadcrumbItemTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "li";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "breadcrumb-item");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ComponentName("dui-breadcrumb-item"),
                "inline-flex items-center",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
