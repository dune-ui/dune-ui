using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using FrameworkSelectTagHelper = Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-select")]
public class SelectTagHelper : FieldInputBaseTagHelper
{
    private readonly IHtmlGenerator _htmlGenerator;
    private readonly ICssClassMerger _classMerger;
    private FrameworkSelectTagHelper? _frameworkTagHelper;

    public SelectTagHelper(IHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
        : base(htmlGenerator, classMerger)
    {
        _htmlGenerator = htmlGenerator ?? throw new ArgumentNullException(nameof(htmlGenerator));
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    [HtmlAttributeName("asp-items")]
    public IEnumerable<SelectListItem>? Items { get; set; }

    public override void Init(TagHelperContext context)
    {
        base.Init(context);

        if (For != null || Items != null)
        {
            _frameworkTagHelper = new FrameworkSelectTagHelper(_htmlGenerator)
            {
                For = For,
                Items = Items,
                ViewContext = ViewContext,
                Name = Name,
            };
            _frameworkTagHelper.Init(context);
        }
    }

    protected override async Task<AutoFieldLayout> RenderInput(
        TagHelperContext context,
        TagHelperOutput output,
        IDictionary<string, object?>? htmlAttributes
    )
    {
        if ((For != null || Items != null) && _frameworkTagHelper != null)
        {
            _frameworkTagHelper.Process(context, output);
        }
        else
        {
            output.Content.SetHtmlContent(await output.GetChildContentAsync());
        }

        output.TagName = "select";
        output.TagMode = TagMode.StartTagAndEndTag;

        var allowMultipleValues =
            output.Attributes.TryGetAttribute("multiple", out var multipleAttribute)
            && (
                multipleAttribute.Value == null
                || (string)multipleAttribute.Value == "true"
                || (string)multipleAttribute.Value == "multiple"
            );

        output.Attributes.SetAttribute(
            "class",
            _classMerger.Merge(
                allowMultipleValues ? "p-2" : "appearance-none h-9 pl-3 pr-9 py-2",
                "border-input focus-visible:border-ring focus-visible:ring-ring/50 aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive dark:bg-input/30 dark:hover:bg-input/50 flex w-fit items-center justify-between gap-2 rounded-md border bg-transparent text-sm whitespace-nowrap shadow-xs transition-[color,box-shadow] outline-none focus-visible:ring-[3px] disabled:cursor-not-allowed disabled:opacity-50",
                allowMultipleValues
                    ? null
                    : "bg-[image:var(--chevron-down-icon-50)] bg-no-repeat bg-position-[center_right_0.75rem] bg-size-[1rem]",
                "[&.input-validation-error]:ring-destructive/20 dark:[&.input-validation-error]:ring-destructive/40 [&.input-validation-error]:border-destructive",
                output.GetUserSuppliedClass()
            )
        );

        return AutoFieldLayout.Vertical;
    }
}
