using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     Displays a progress bar's value, rendered as a <c>&lt;span&gt;</c>; falls back to the
///     computed completion percentage when no content is supplied.
/// </summary>
[HtmlTargetElement("dui-progress-value")]
public class ProgressValueTagHelper : DuneUITagHelperBase
{
    public ProgressValueTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.Add("data-slot", "progress-value");
        output.Attributes.Add(
            "class",
            ClassMerger.Merge(new ThemeToken("dui-progress-value"), output.GetUserSuppliedClass())
        );

        var content = await output.GetChildContentAsync();
        if (content.IsEmptyOrWhiteSpace)
        {
            output.Content.AppendHtml(GetPercentageCompleted().ToString("P0"));
        }
        else
        {
            output.Content.AppendHtml(content);
        }
    }

    private double GetPercentageCompleted()
    {
        var (minimum, maximum, value) = GetValues();

        return (double)(value - minimum) / (maximum - minimum);
    }

    private (int Minimum, int Maximum, int Value) GetValues()
    {
        var parentProgressTagHelper = GetParentTagHelper<ProgressTagHelper>();
        return parentProgressTagHelper == null
            ? (0, 100, 0)
            : (
                parentProgressTagHelper.Minimum ?? 0,
                parentProgressTagHelper.Maximum ?? 100,
                parentProgressTagHelper.Value ?? 0
            );
    }
}
