using System.Collections;
using System.Diagnostics;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-select")]
public class SelectTagHelper(IStellarHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
    : FieldInputBaseTagHelper(htmlGenerator, classMerger)
{
    private bool _allowMultiple;
    private ICollection<string> _currentValues;

    [HtmlAttributeName("asp-items")]
    public IEnumerable<SelectListItem>? Items { get; set; }

    public override void Init(TagHelperContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (For == null)
        {
            context.Items[typeof(SelectTagHelper)] = null;
            return;
        }

        // Note null or empty For.Name is allowed because TemplateInfo.HtmlFieldPrefix may be sufficient.
        // IHtmlGenerator will enforce name requirements.
        // if (For.Metadata == null)
        // {
        //     throw new InvalidOperationException(Resources.FormatTagHelpers_NoProvidedMetadata(
        //         "<select>",
        //         ForAttributeName,
        //         nameof(IModelMetadataProvider),
        //         For.Name));
        // }

        // Base allowMultiple on the instance or declared type of the expression to avoid a
        // "SelectExpressionNotEnumerable" InvalidOperationException during generation.
        // Metadata.IsEnumerableType is similar but does not take runtime type into account.
        var realModelType = For.ModelExplorer.ModelType;
        _allowMultiple =
            realModelType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(realModelType);
        _currentValues = htmlGenerator.GetCurrentValues(
            ViewContext,
            For.ModelExplorer,
            For.Name,
            _allowMultiple
        );

        // Whether or not (not being highly unlikely) we generate anything, could update contained <option/>
        // elements. Provide selected values for <option/> tag helpers.
        var currentValues = _currentValues == null ? null : new CurrentValues(_currentValues);
        context.Items[typeof(SelectTagHelper)] = currentValues;
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
                    allowMultiple: false,
                    currentValues: null,
                    htmlAttributes: null
                )
                : htmlGenerator.GenerateSelect(
                    ViewContext,
                    For.ModelExplorer,
                    optionLabel: null,
                    expression: For.Name,
                    selectList: items,
                    allowMultiple: false,
                    htmlAttributes: null
                );

        output.MergeAttributes(tagBuilder);

        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "appearance-none border-input focus-visible:border-ring focus-visible:ring-ring/50 aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive dark:bg-input/30 dark:hover:bg-input/50 flex w-fit items-center justify-between gap-2 rounded-md border bg-transparent pl-3 pr-9 py-2 text-sm whitespace-nowrap shadow-xs transition-[color,box-shadow] outline-none focus-visible:ring-[3px] disabled:cursor-not-allowed disabled:opacity-50 h-9",
                "bg-[image:var(--chevron-down-icon-50)] bg-no-repeat bg-position-[center_right_0.75rem] bg-size-[1rem]",
                "[&.input-validation-error]:ring-destructive/20 dark:[&.input-validation-error]:ring-destructive/40 [&.input-validation-error]:border-destructive",
                output.GetUserSuppliedClass()
            )
        );
        // Workaround for https://github.com/desmondinho/tailwind-merge-dotnet/issues/8 to ensure class is added
        output.AddClass("bg-position-[center_right_0.75rem]", HtmlEncoder.Default);

        output.Content.SetHtmlContent(await output.GetChildContentAsync());

        if (tagBuilder.HasInnerHtml)
        {
            output.PostContent.AppendHtml(tagBuilder.InnerHtml);
        }
    }
}

internal sealed class CurrentValues(ICollection<string> values)
{
    public ICollection<string> Values { get; } = values;

    public ICollection<string> ValuesAndEncodedValues { get; set; }
}
