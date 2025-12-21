using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-input-group-input")]
public class InputGroupInputTagHelper : StellarTagHelper
{
    private readonly IHtmlGenerator _htmlGenerator;

    /// <summary>
    ///     An expression to be evaluated against the current model.
    /// </summary>
    [HtmlAttributeName("asp-for")]
    public ModelExpression? For { get; set; }

    [HtmlAttributeName("asp-format")]
    public string? Format { get; set; }

    [HtmlAttributeName("form")]
    public string? FormName { get; set; }

    [HtmlAttributeName("type")]
    public string? InputTypeName { get; set; }

    [HtmlAttributeName("value")]
    public string? Value { get; set; }

    /// <summary>
    ///     Gets the <see cref="ViewContext" /> of the executing view.
    /// </summary>
    [HtmlAttributeNotBound]
    [ViewContext]
    public required ViewContext ViewContext { get; set; }

    public InputGroupInputTagHelper(
        ThemeManager themeManager,
        IHtmlGenerator htmlGenerator,
        ICssClassMerger classMerger
    )
        : base(themeManager, classMerger)
    {
        _htmlGenerator = htmlGenerator ?? throw new ArgumentNullException(nameof(htmlGenerator));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.SetAttribute("data-slot", "input-group-control");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-input-group-input"),
                "flex-1",
                output.GetUserSuppliedClass()
            )
        );

        var inputTagHelper = new InputTagHelper(ThemeManager, _htmlGenerator, ClassMerger)
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
