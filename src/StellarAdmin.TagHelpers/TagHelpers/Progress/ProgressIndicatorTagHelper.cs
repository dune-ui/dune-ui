using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-progress-indicator")]
public class ProgressIndicatorTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "progress-indicator");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "bg-primary h-full w-full flex-1 transition-all",
                output.GetUserSuppliedClass()
            )
        );
        output.Attributes.SetAttribute(
            "style",
            $"transform: translateX(-{100 - GetPercentageCompleted()}%)"
        );

        return Task.CompletedTask;
    }

    private int GetPercentageCompleted()
    {
        (int minimum, int maximum, int value) = GetValues();

        return (int)Math.Round(((double)(value - minimum) / (maximum - minimum)) * 100);
    }

    private (int Minimum, int Maximum, int Value) GetValues()
    {
        var parentProgressTagHelper = GetParentTagHelper<ProgressTagHelper>();
        return parentProgressTagHelper == null
            ? (0, 100, 0)
            : (
                parentProgressTagHelper.Minimum,
                parentProgressTagHelper.Maximum,
                parentProgressTagHelper.Value
            );
    }
}
