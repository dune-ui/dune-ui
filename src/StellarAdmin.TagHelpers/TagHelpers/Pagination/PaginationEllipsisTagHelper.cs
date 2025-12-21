using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Icons;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-pagination-ellipsis")]
public class PaginationEllipsisTagHelper : StellarTagHelper
{
    private readonly IIconManager _iconManager;

    public PaginationEllipsisTagHelper(
        ThemeManager themeManager,
        ICssClassMerger classMerger,
        IIconManager iconManager
    )
        : base(themeManager, classMerger)
    {
        _iconManager = iconManager ?? throw new ArgumentNullException(nameof(iconManager));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("aria-hidden", "true");
        output.Attributes.SetAttribute("data-slot", "pagination-ellipsis");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-pagination-ellipsis"),
                "flex items-center justify-center",
                output.GetUserSuppliedClass()
            )
        );

        var content = await output.GetChildContentAsync();

        if (!content.IsEmptyOrWhiteSpace)
        {
            output.Content.AppendHtml(content);
        }
        else
        {
            // Render the icon
            var iconOutput = new TagHelperOutput(
                "svg",
                [new TagHelperAttribute("class", "size-4")],
                (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
            );
            var iconTagHelper = new IconTagHelper(ThemeManager, ClassMerger, _iconManager)
            {
                Name = "ellipsis",
            };
            await iconTagHelper.ProcessAsync(context, iconOutput);
            output.Content.AppendHtml(iconOutput);

            // Render the text
            var textBlockTagBuilder = new TagBuilder("span");
            textBlockTagBuilder.AddCssClass("sr-only");
            textBlockTagBuilder.InnerHtml.AppendHtml("More pages");
            output.Content.AppendHtml(textBlockTagBuilder);
        }
    }
}
