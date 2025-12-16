using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-field")]
public class FieldTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public FieldTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    [HtmlAttributeName("orientation")]
    public FieldOrientation Orientation { get; set; } = FieldOrientation.Vertical;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        // The MergeAttributes call below does a simple concatenation of the classes, but this is not correct.
        // We need to do proper merging using ICssClassMerger, which will happen inside the FieldTagBuilder.
        // To ensure correct behaviour we need to first extract the user supplied class so we can pass it to
        // FieldTagBuilder and then clear it before calling MergeAttribute() to prevent a double merge.
        var userSuppliedClass = output.GetUserSuppliedClass();
        output.Attributes.SetAttribute("class", string.Empty);

        var tagBuilder = new FieldTagBuilder(_classMerger, Orientation, userSuppliedClass);

        output.TagName = tagBuilder.TagName;
        output.TagMode = TagMode.StartTagAndEndTag;

        output.MergeAttributes(tagBuilder);

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
