using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-skeleton")]
public class SkeletonTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public SkeletonTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "skeleton");
        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge("bg-accent animate-pulse rounded-md", output.GetUserSuppliedClass())
        );

        return Task.CompletedTask;
    }
}
