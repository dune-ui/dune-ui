using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Icons;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-spinner")]
public class SpinnerTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;
    private readonly IIconManager _iconManager;

    public SpinnerTagHelper(
        ThemeManager themeManager,
        ICssClassMerger classMerger,
        IIconManager iconManager
    )
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
        _iconManager = iconManager ?? throw new ArgumentNullException(nameof(iconManager));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var iconTagHelper = new IconTagHelper(ThemeManager, ClassMerger, _iconManager)
        {
            Name = "loader-circle",
        };
        await iconTagHelper.ProcessAsync(context, output);

        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge("animate-spin", output.GetUserSuppliedClass())
        );
    }
}
