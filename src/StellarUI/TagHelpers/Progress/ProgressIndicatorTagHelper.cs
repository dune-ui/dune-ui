using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-progress-indicator")]
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
        (int minimum, int maximum, int value) = GetValue();

        return (int)Math.Round(((double)(value - minimum) / (maximum - minimum)) * 100);
    }

    private (int Minimum, int Maximum, int Value) GetValue()
    {
        var currentParentTagHelper = ParentTagHelper;
        while (currentParentTagHelper is not null)
        {
            if (currentParentTagHelper is ProgressTagHelper parentTagHelper)
            {
                return (parentTagHelper.Minimum, parentTagHelper.Maximum, parentTagHelper.Value);
            }

            currentParentTagHelper = currentParentTagHelper.ParentTagHelper;
        }

        return (0, 100, 0);
    }
}
