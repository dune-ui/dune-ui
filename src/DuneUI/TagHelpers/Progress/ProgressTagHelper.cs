using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A progress bar that visualizes the completion of a task as a filled track. Compose
///     it with the label and value subcomponents.
/// </summary>
[HtmlTargetElement("dui-progress")]
public class ProgressTagHelper : DuneUITagHelperBase
{
    public ProgressTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    /// <summary>
    ///     The value representing full completion.
    /// </summary>
    /// <remarks>
    ///     Defaults to <c>100</c>.
    /// </remarks>
    [HtmlAttributeName("maximum")]
    public int? Maximum { get; set; }

    /// <summary>
    ///     The value representing no completion.
    /// </summary>
    /// <remarks>
    ///     Defaults to <c>0</c>.
    /// </remarks>
    [HtmlAttributeName("minimum")]
    public int? Minimum { get; set; }

    /// <summary>
    ///     The current progress value, between the minimum and maximum.
    /// </summary>
    /// <remarks>
    ///     Defaults to <c>0</c>.
    /// </remarks>
    [HtmlAttributeName("value")]
    public int? Value { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveMinimum = Minimum ?? 0;
        var effectiveMaximum = Maximum ?? 100;
        var effectiveValue = Value ?? 0;

        if (effectiveMinimum >= effectiveMaximum)
        {
            throw new ArgumentOutOfRangeException(
                nameof(Minimum),
                "Minimum must be less than Maximum"
            );
        }

        if (effectiveValue < effectiveMinimum || effectiveValue > effectiveMaximum)
        {
            throw new ArgumentOutOfRangeException(
                nameof(Value),
                "Must be between Minimum and Maximum"
            );
        }

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "progress");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-progress-root"),
                "flex flex-wrap gap-3",
                output.GetUserSuppliedClass()
            )
        );

        var trackTagBuilder = new TagBuilder("div");
        trackTagBuilder.Attributes.Add("data-slot", "progress-track");
        trackTagBuilder.Attributes.Add(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-progress-track"),
                "relative flex w-full items-center overflow-x-hidden"
            )
        );

        var indicatorTagBuilder = new TagBuilder("div");
        indicatorTagBuilder.Attributes.Add("data-slot", "progress-indicator");
        indicatorTagBuilder.Attributes.Add(
            "class",
            ClassMerger.Merge(new ThemeToken("dui-progress-indicator"), "h-full transition-all")
        );
        indicatorTagBuilder.Attributes.Add(
            "style",
            $"inset-inline-start: 0px; height: inherit; width: {GetPercentageCompleted(effectiveMinimum, effectiveMaximum, effectiveValue)}%;"
        );
        trackTagBuilder.InnerHtml.AppendHtml(indicatorTagBuilder);
        output.PostContent.AppendHtml(trackTagBuilder);
    }

    private int GetPercentageCompleted(int minimum, int maximum, int value)
    {
        return (int)Math.Round(((double)(value - minimum) / (maximum - minimum)) * 100);
    }
}
