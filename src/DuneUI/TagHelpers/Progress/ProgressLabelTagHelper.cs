using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A text label for a progress bar, rendered as a <c>&lt;span&gt;</c>.
/// </summary>
[HtmlTargetElement("dui-progress-label")]
public class ProgressLabelTagHelper : DuneUITagHelperBase
{
    public ProgressLabelTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.Add("data-slot", "progress-label");
        output.Attributes.Add(
            "class",
            ClassMerger.Merge(new ThemeToken("dui-progress-label"), output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
