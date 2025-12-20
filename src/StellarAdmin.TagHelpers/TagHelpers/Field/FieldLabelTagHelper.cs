using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-field-label")]
public class FieldLabelTagHelper : StellarTagHelper
{
    private readonly IHtmlGenerator _htmlGenerator;

    public FieldLabelTagHelper(
        ThemeManager themeManager,
        IHtmlGenerator htmlGenerator,
        ICssClassMerger classMerger
    )
        : base(themeManager, classMerger)
    {
        _htmlGenerator = htmlGenerator ?? throw new ArgumentNullException(nameof(htmlGenerator));
    }

    private const string ForAttributeName = "asp-for";

    /// <summary>
    /// An expression to be evaluated against the current model.
    /// </summary>
    [HtmlAttributeName(ForAttributeName)]
    public ModelExpression? For { get; set; }

    /// <summary>
    /// Gets the <see cref="ViewContext"/> of the executing view.
    /// </summary>
    [HtmlAttributeNotBound]
    [ViewContext]
    public required ViewContext ViewContext { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.SetAttribute("data-slot", "field-label");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-field-label"),
                "group/field-label peer/field-label flex w-fit leading-snug",
                "has-[>[data-slot=field]]:w-full has-[>[data-slot=field]]:flex-col",
                output.GetUserSuppliedClass()
            )
        );

        var labelTagHelper = new LabelTagHelper(ThemeManager, _htmlGenerator, ClassMerger)
        {
            For = For,
            ViewContext = ViewContext,
        };
        await labelTagHelper.ProcessAsync(context, output);
    }
}
