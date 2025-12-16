using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-input-group-input")]
public class InputGroupInputTagHelper : StellarTagHelper
{
    private readonly IHtmlGenerator _htmlGenerator;
    private readonly ICssClassMerger _classMerger;

    /// <summary>
    ///     An expression to be evaluated against the current model.
    /// </summary>
    [HtmlAttributeName("asp-for")]
    public ModelExpression? For { get; set; }

    [HtmlAttributeName("form")]
    public string? FormName { get; set; }

    /// <summary>
    ///     Gets the <see cref="ViewContext" /> of the executing view.
    /// </summary>
    [HtmlAttributeNotBound]
    [ViewContext]
    public required ViewContext ViewContext { get; set; }

    [HtmlAttributeName("type")]
    public string? InputTypeName { get; set; }

    [HtmlAttributeName("value")]
    public string? Value { get; set; }

    [HtmlAttributeName("asp-format")]
    public string? Format { get; set; }

    public InputGroupInputTagHelper(
        ThemeManager themeManager,
        IHtmlGenerator htmlGenerator,
        ICssClassMerger classMerger
    )
        : base(themeManager)
    {
        _htmlGenerator = htmlGenerator ?? throw new ArgumentNullException(nameof(htmlGenerator));
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.SetAttribute("data-slot", "input-group-control");
        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge(
                "flex-1 rounded-none border-0 bg-transparent shadow-none focus-visible:ring-0 dark:bg-transparent",
                output.GetUserSuppliedClass()
            )
        );

        var inputTagHelper = new InputTagHelper(ThemeManager, _htmlGenerator, _classMerger)
        {
            ViewContext = ViewContext,
            For = For,
            InputTypeName = InputTypeName,
            Format = Format,
            FormName = FormName,
            Value = Value,
            ShouldRenderField = false,
        };
        await inputTagHelper.ProcessAsync(context, output);
    }
}
