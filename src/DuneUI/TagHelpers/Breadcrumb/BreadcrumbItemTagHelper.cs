using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A single item within the breadcrumb trail, rendered as a <c>&lt;li&gt;</c>; wraps a
///     link, page, or separator.
/// </summary>
[HtmlTargetElement("dui-breadcrumb-item")]
public class BreadcrumbItemTagHelper : DuneUITagHelperBase
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
                new ThemeToken("dui-breadcrumb-item"),
                "inline-flex items-center",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
