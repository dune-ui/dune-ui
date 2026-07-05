using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     The current page in the breadcrumb trail, rendered as a non-interactive
///     <c>&lt;span&gt;</c> marked with <c>aria-current="page"</c>.
/// </summary>
[HtmlTargetElement("dui-breadcrumb-page")]
public class BreadcrumbPageTagHelper : DuneUITagHelperBase
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
            BuildClassString(new ThemeToken("dui-breadcrumb-page"), output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
