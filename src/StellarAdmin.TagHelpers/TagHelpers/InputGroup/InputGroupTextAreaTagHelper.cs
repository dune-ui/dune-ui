using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-input-group-textarea")]
public class InputGroupTextAreaTagHelper : StellarTagHelper
{
    private readonly IHtmlGenerator _htmlGenerator;
    private readonly ICssClassMerger _classMerger;

    /// <summary>
    ///     An expression to be evaluated against the current model.
    /// </summary>
    [HtmlAttributeName("asp-for")]
    public ModelExpression? For { get; set; }

    /// <summary>
    ///     Gets the <see cref="ViewContext" /> of the executing view.
    /// </summary>
    [HtmlAttributeNotBound]
    [ViewContext]
    public required ViewContext ViewContext { get; set; }

    public InputGroupTextAreaTagHelper(IHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
    {
        _htmlGenerator = htmlGenerator;
        _classMerger = classMerger;
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.SetAttribute("data-slot", "input-group-control");
        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge(
                "flex-1 resize-none rounded-none border-0 bg-transparent py-3 shadow-none focus-visible:ring-0 dark:bg-transparent",
                output.GetUserSuppliedClass()
            )
        );

        var textareaTagHelper = new TextareaTagHelper(_htmlGenerator, _classMerger)
        {
            ViewContext = ViewContext,
            For = For,
            ShouldRenderField = false,
        };
        await textareaTagHelper.ProcessAsync(context, output);
    }
}
