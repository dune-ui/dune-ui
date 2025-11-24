using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-tab-list")]
public class TabListTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public TabListOrientation? Orientation { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveOrientation = Orientation ?? TabListOrientation.Horizontal;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "tabs-list");

        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "bg-muted text-muted-foreground inline-flex items-center justify-center rounded-lg p-[3px]",
                effectiveOrientation == TabListOrientation.Horizontal
                    ? "h-9 w-fit"
                    : "flex-col [&_[data-slot=tabs-trigger]]:w-full [&_[data-slot=tabs-trigger]]:justify-start",
                output.GetUserSuppliedClass()
            )
        );

        return base.ProcessAsync(context, output);
    }
}
