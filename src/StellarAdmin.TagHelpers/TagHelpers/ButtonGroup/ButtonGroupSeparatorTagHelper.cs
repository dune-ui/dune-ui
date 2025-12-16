using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-button-group-separator")]
public class ButtonGroupSeparatorTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public ButtonGroupSeparatorTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    [HtmlAttributeName("orientation")]
    public SeparatorOrientation Orientation { get; set; } = SeparatorOrientation.Vertical;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.SetAttribute("data-slot", "button-group-separator");
        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge(
                "bg-input relative !m-0 self-stretch data-[orientation=vertical]:h-auto",
                output.GetUserSuppliedClass()
            )
        );

        var separatorTagHelper = new SeparatorTagHelper(ThemeManager, _classMerger)
        {
            Orientation = Orientation,
        };
        await separatorTagHelper.ProcessAsync(context, output);
    }
}
