using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-select")]
public class SelectTagHelper(IStellarHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
    : FieldInputBaseTagHelper(htmlGenerator, classMerger)
{
    private bool _allowMultipleValues;

    internal ICollection<string>? SelectedValues { get; private set; } = null;
    internal HashSet<string>? SelectedRawAndEncodedValues { get; private set; } = null;

    [HtmlAttributeName("asp-items")]
    public IEnumerable<SelectListItem>? Items { get; set; }

    public override void Init(TagHelperContext context)
    {
        base.Init(context);

        // If we are not doing model binding, exit early
        if (For == null)
        {
            return;
        }

        // Check whether model allows for binding multiple values
        var underlyingModelType = For.ModelExplorer.ModelType;
        _allowMultipleValues =
            underlyingModelType != typeof(string)
            && typeof(IEnumerable).IsAssignableFrom(underlyingModelType);
        SelectedValues = htmlGenerator.GetCurrentValues(
            ViewContext,
            For.ModelExplorer,
            For.Name,
            _allowMultipleValues
        );
        SelectedRawAndEncodedValues = SelectedValues
            .Union(SelectedValues.Select(htmlGenerator.Encode))
            .ToHashSet();
    }

    protected override async Task RenderInput(
        TagHelperContext context,
        TagHelperOutput output,
        IDictionary<string, object?>? htmlAttributes
    )
    {
        var items = Items ?? [];

        output.TagName = "select";
        output.TagMode = TagMode.StartTagAndEndTag;

        var tagBuilder =
            For == null
                ? htmlGenerator.GenerateSelect(
                    optionLabel: null,
                    selectList: items,
                    allowMultiple: _allowMultipleValues,
                    currentValues: null,
                    htmlAttributes: null
                )
                : htmlGenerator.GenerateSelect(
                    ViewContext,
                    For.ModelExplorer,
                    optionLabel: null,
                    expression: For.Name,
                    selectList: items,
                    currentValues: SelectedValues,
                    allowMultiple: _allowMultipleValues,
                    htmlAttributes: null
                );

        output.MergeAttributes(tagBuilder);

        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                _allowMultipleValues ? "p-2" : "appearance-none h-9 pl-3 pr-9 py-2",
                "border-input focus-visible:border-ring focus-visible:ring-ring/50 aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive dark:bg-input/30 dark:hover:bg-input/50 flex w-fit items-center justify-between gap-2 rounded-md border bg-transparent text-sm whitespace-nowrap shadow-xs transition-[color,box-shadow] outline-none focus-visible:ring-[3px] disabled:cursor-not-allowed disabled:opacity-50",
                _allowMultipleValues
                    ? null
                    : "bg-[image:var(--chevron-down-icon-50)] bg-no-repeat bg-position-[center_right_0.75rem] bg-size-[1rem]",
                "[&.input-validation-error]:ring-destructive/20 dark:[&.input-validation-error]:ring-destructive/40 [&.input-validation-error]:border-destructive",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.SetHtmlContent(await output.GetChildContentAsync());

        if (tagBuilder.HasInnerHtml)
        {
            output.PostContent.AppendHtml(tagBuilder.InnerHtml);
        }
    }
}
