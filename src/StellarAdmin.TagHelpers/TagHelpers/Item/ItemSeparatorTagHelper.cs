using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-item-separator")]
public class ItemSeparatorTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public ItemSeparatorTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    [HtmlAttributeName("is-decorative")]
    public bool IsDecorative { get; set; } = true;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge("my-0", GetUserSpecifiedClass(output))
        );
        output.Attributes.SetAttribute("data-slot", "item-separator");

        var separatorTagHelper = new SeparatorTagHelper(ThemeManager, _classMerger)
        {
            Orientation = SeparatorOrientation.Horizontal,
            IsDecorative = IsDecorative,
        };
        await separatorTagHelper.ProcessAsync(context, output);
    }
}
