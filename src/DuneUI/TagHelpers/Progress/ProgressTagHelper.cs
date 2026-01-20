using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-progress")]
public class ProgressTagHelper : DuneUITagHelperBase
{
    public ProgressTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    [HtmlAttributeName("maximum")]
    public int? Maximum { get; set; }

    [HtmlAttributeName("minimum")]
    public int? Minimum { get; set; }

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
                new ComponentName("dui-progress-root"),
                "flex flex-wrap gap-3",
                output.GetUserSuppliedClass()
            )
        );

        var trackTagBuilder = new TagBuilder("div");
        trackTagBuilder.Attributes.Add("data-slot", "progress-track");
        trackTagBuilder.Attributes.Add(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-progress-track"),
                "relative flex w-full items-center overflow-x-hidden"
            )
        );

        var indicatorTagBuilder = new TagBuilder("div");
        indicatorTagBuilder.Attributes.Add("data-slot", "progress-indicator");
        indicatorTagBuilder.Attributes.Add(
            "class",
            ClassMerger.Merge(new ComponentName("dui-progress-indicator"), "h-full transition-all")
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
