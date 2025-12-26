using System.Globalization;
using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-textarea")]
public class TextareaTagHelper : FieldInputBaseTagHelper
{
    private readonly IHtmlGenerator _htmlGenerator;
    private readonly ICssClassMerger _classMerger;

    public TextareaTagHelper(
        ThemeManager themeManager,
        IHtmlGenerator htmlGenerator,
        ICssClassMerger classMerger
    )
        : base(themeManager, htmlGenerator, classMerger)
    {
        _htmlGenerator = htmlGenerator ?? throw new ArgumentNullException(nameof(htmlGenerator));
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    protected override async Task<AutoFieldLayout> RenderInput(
        TagHelperContext context,
        TagHelperOutput output,
        IDictionary<string, object?>? htmlAttributes
    )
    {
        output.TagName = "textarea";
        output.TagMode = TagMode.StartTagAndEndTag;

        var tagBuilder =
            For == null
                ? GenerateTextAreaTagBuilder(rows: 0, columns: 0, htmlAttributes: htmlAttributes)
                : _htmlGenerator.GenerateTextArea(
                    ViewContext,
                    For.ModelExplorer,
                    For.Name,
                    rows: 0,
                    columns: 0,
                    htmlAttributes: htmlAttributes
                );

        output.MergeAttributes(tagBuilder);

        if (!output.Attributes.ContainsName("data-slot"))
        {
            output.Attributes.SetAttribute("data-slot", "textarea");
        }

        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge(
                new ComponentName("dui-textarea"),
                "placeholder:text-muted-foreground flex field-sizing-content min-h-16 w-full outline-none disabled:cursor-not-allowed disabled:opacity-50",
                output.GetUserSuppliedClass()
            )
        );

        var childContent = await output.GetChildContentAsync();
        if (childContent.IsEmptyOrWhiteSpace)
        {
            if (tagBuilder.HasInnerHtml)
            {
                output.Content.SetHtmlContent(tagBuilder.InnerHtml);
            }
        }
        else
        {
            output.Content.SetHtmlContent(childContent);
        }

        return AutoFieldLayout.Vertical;
    }

    private TagBuilder GenerateTextAreaTagBuilder(
        int rows = 0,
        int columns = 0,
        IDictionary<string, object?>? htmlAttributes = null
    )
    {
        if (rows < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rows));
        }

        if (columns < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(columns));
        }

        var tagBuilder = new TagBuilder("textarea");
        if (htmlAttributes != null)
        {
            tagBuilder.MergeAttributes(htmlAttributes, true);
        }

        if (rows > 0)
        {
            tagBuilder.MergeAttribute("rows", rows.ToString(CultureInfo.InvariantCulture), true);
        }

        if (columns > 0)
        {
            tagBuilder.MergeAttribute("cols", columns.ToString(CultureInfo.InvariantCulture), true);
        }

        return tagBuilder;
    }
}
