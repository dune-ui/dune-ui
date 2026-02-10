using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("*", Attributes = "as-child")]
public class AsChildTagHelper : DuneUITagHelperBase
{
    private readonly IAttributeMerger _attributeMerger;

    [HtmlAttributeName("as-child")]
    public bool AsChild { get; set; }

    public override int Order => 1000;

    public AsChildTagHelper(
        ThemeManager themeManager,
        ICssClassMerger classMerger,
        IAttributeMerger attributeMerger
    )
        : base(themeManager, classMerger)
    {
        _attributeMerger =
            attributeMerger ?? throw new ArgumentNullException(nameof(attributeMerger));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (!AsChild)
            return;

        var contentWithMergedAttributes = await _attributeMerger.MergeAttributes(
            await output.GetChildContentAsync(),
            output.Attributes.Where(a =>
                !a.Name.Equals("as-child", StringComparison.OrdinalIgnoreCase)
            )
        );

        if (!contentWithMergedAttributes.IsEmptyOrWhiteSpace)
        {
            output.TagName = null;
            output.SuppressOutput();

            output.Content.AppendHtml(contentWithMergedAttributes);
        }
    }
}
