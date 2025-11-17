using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-textarea")]
public class TextareaTagHelper : FieldInputBaseTagHelper
{
    private readonly IStellarHtmlGenerator _htmlGenerator;
    private readonly ICssClassMerger _classMerger;

    public TextareaTagHelper(IStellarHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
        : base(htmlGenerator, classMerger)
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
                ? _htmlGenerator.GenerateTextArea(
                    rows: 0,
                    columns: 0,
                    htmlAttributes: htmlAttributes
                )
                : _htmlGenerator.GenerateTextArea(
                    ViewContext,
                    For.ModelExplorer,
                    For.Name,
                    rows: 0,
                    columns: 0,
                    htmlAttributes: htmlAttributes
                );

        output.MergeAttributes(tagBuilder);

        output.Attributes.SetAttribute("data-slot", "textarea");
        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge(
                "border-input placeholder:text-muted-foreground focus-visible:border-ring focus-visible:ring-ring/50 aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive dark:bg-input/30 flex field-sizing-content min-h-16 w-full rounded-md border bg-transparent px-3 py-2 text-base shadow-xs transition-[color,box-shadow] outline-none focus-visible:ring-[3px] disabled:cursor-not-allowed disabled:opacity-50 md:text-sm",
                "[&.input-validation-error]:ring-destructive/20 dark:[&.input-validation-error]:ring-destructive/40 [&.input-validation-error]:border-destructive",
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
}
