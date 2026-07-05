using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A region within an alert for interactive elements such as buttons or links.
/// </summary>
[HtmlTargetElement("dui-alert-action")]
public class AlertAction : DuneUITagHelperBase
{
    public AlertAction(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "alert-action");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(new ThemeToken("dui-alert-action"), output.GetUserSuppliedClass())
        );

        output.Content.SetHtmlContent(await output.GetChildContentAsync());
    }
}
