using DuneUI.Icons;
using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-spinner")]
public class SpinnerTagHelper : DuneUITagHelperBase
{
    private readonly ICssClassMerger _classMerger;
    private readonly IIconManager _iconManager;

    public SpinnerTagHelper(
        ThemeManager themeManager,
        ICssClassMerger classMerger,
        IIconManager iconManager
    )
        : base(themeManager, classMerger)
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

        output.Attributes.SetAttribute("role", "status");
        output.Attributes.SetAttribute("aria-label", "Loading");
        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge("size-4 animate-spin", output.GetUserSuppliedClass())
        );
    }
}
