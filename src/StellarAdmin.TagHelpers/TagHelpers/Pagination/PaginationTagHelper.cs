using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-pagination")]
public class PaginationTagHelper : StellarTagHelper
{
    public PaginationTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "nav";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("role", "navigation");
        output.Attributes.SetAttribute("aria-label", "pagination");
        output.Attributes.SetAttribute("data-slot", "pagination");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-pagination"),
                "mx-auto flex w-full justify-center",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
