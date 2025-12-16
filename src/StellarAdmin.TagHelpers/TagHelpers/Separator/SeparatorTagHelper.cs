using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-separator")]
public class SeparatorTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public SeparatorTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    [HtmlAttributeName("is-decorative")]
    public bool IsDecorative { get; set; } = true;

    [HtmlAttributeName("orientation")]
    public SeparatorOrientation Orientation { get; set; } = SeparatorOrientation.Horizontal;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge(
                "bg-border shrink-0 data-[orientation=horizontal]:h-px data-[orientation=horizontal]:w-full data-[orientation=vertical]:h-full data-[orientation=vertical]:w-px",
                output.GetUserSuppliedClass()
            )
        );
        output.Attributes.SetAttribute("data-role", IsDecorative ? "none" : "separator");
        output.Attributes.SetAttribute("data-orientation", GetOrientationAttributeText());
    }

    private string GetOrientationAttributeText() =>
        Orientation switch
        {
            SeparatorOrientation.Horizontal => "horizontal",
            SeparatorOrientation.Vertical => "vertical",
            _ => Orientation.ToString(),
        };
}
