using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-tab-list")]
public class TabListTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public TabListTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    public TabListOrientation? Orientation { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveOrientation = Orientation ?? TabListOrientation.Horizontal;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "tabs-list");

        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge(
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
