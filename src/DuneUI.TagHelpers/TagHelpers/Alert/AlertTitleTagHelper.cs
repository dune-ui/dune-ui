using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-alert-title")]
public class AlertTitleTagHelper : DuneUITagHelperBase
{
    public AlertTitleTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "alert-title");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ComponentName("dui-alert-title"),
                "[&_a]:hover:text-foreground [&_a]:underline [&_a]:underline-offset-3",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.SetHtmlContent(await output.GetChildContentAsync());
    }
}
