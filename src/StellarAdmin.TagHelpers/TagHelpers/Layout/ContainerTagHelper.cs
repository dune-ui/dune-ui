using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-container")]
public class ContainerTagHelper : StellarTagHelper
{
    public ContainerTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge("container mx-auto sm:px-6 lg:px-8", output.GetUserSuppliedClass())
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
