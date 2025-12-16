using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-alert-description")]
public class AlertDescriptionTagHelper : StellarTagHelper
{
    public AlertDescriptionTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "alert-description");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ComponentName("dui-alert-description"),
                "[&_a]:hover:text-foreground [&_a]:underline [&_a]:underline-offset-3",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.SetHtmlContent(await output.GetChildContentAsync());
    }
}
