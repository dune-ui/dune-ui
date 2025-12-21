using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Icons;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-pagination-next")]
public class PaginationNextTagHelper : PaginationLinkTagHelper
{
    private readonly IIconManager _iconManager;

    public PaginationNextTagHelper(
        ThemeManager themeManager,
        IHtmlGenerator htmlGenerator,
        ICssClassMerger classMerger,
        IIconManager iconManager
    )
        : base(themeManager, htmlGenerator, classMerger)
    {
        _iconManager = iconManager;
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        await RenderLink(context, output);

        var content = await output.GetChildContentAsync();

        if (!content.IsEmptyOrWhiteSpace)
        {
            output.Content.AppendHtml(content);
        }
        else
        {
            // Render the text
            var textBlockTagBuilder = new TagBuilder("span");
            textBlockTagBuilder.AddCssClass("hidden sm:block");
            textBlockTagBuilder.InnerHtml.AppendHtml("Next");
            output.Content.AppendHtml(textBlockTagBuilder);

            // Render the icon
            var iconOutput = new TagHelperOutput(
                "svg",
                [],
                (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
            );
            var iconTagHelper = new IconTagHelper(ThemeManager, ClassMerger, _iconManager)
            {
                Name = "chevron-right",
            };
            await iconTagHelper.ProcessAsync(context, iconOutput);
            output.Content.AppendHtml(iconOutput);
        }
    }
}
