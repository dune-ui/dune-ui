using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using FrameworkInputTagHelper = Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-input", TagStructure = TagStructure.WithoutEndTag)]
public class InputTagHelper(IStellarHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
    : FieldInputBaseTagHelper(htmlGenerator, classMerger)
{
    [HtmlAttributeName("form")]
    public string? FormName { get; set; }

    [HtmlAttributeName("asp-format")]
    public string? Format { get; set; }

    [HtmlAttributeName("type")]
    public string? InputTypeName { get; set; }

    [HtmlAttributeName("value")]
    public string? Value { get; set; }

    protected override Task<AutoFieldLayout> RenderInput(
        TagHelperContext context,
        TagHelperOutput output,
        IDictionary<string, object?>? htmlAttributes
    )
    {
        if (For == null)
        {
            if (InputTypeName != null)
            {
                output.CopyHtmlAttribute("type", context);
            }

            if (Value != null)
            {
                output.CopyHtmlAttribute("value", context);
            }

            if (FormName != null)
            {
                output.CopyHtmlAttribute("form", context);
            }
        }
        else
        {
            var inputTagHelper = new FrameworkInputTagHelper(htmlGenerator)
            {
                For = For,
                Format = Format,
                FormName = FormName,
                InputTypeName = InputTypeName,
                Value = Value,
                Name = Name,
                ViewContext = ViewContext,
            };
            inputTagHelper.Process(context, output);
        }

        var type = output.Attributes.TryGetAttribute("type", out var typeAttribute)
            ? typeAttribute.Value
            : null;

        string[] classNames = type switch
        {
            "checkbox" =>
            [
                "appearance-none border-input dark:bg-input/30 checked:bg-primary dark:checked:bg-primary checked:border-primary focus-visible:border-ring focus-visible:ring-ring/50 aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive size-4 shrink-0 rounded-[4px] border shadow-xs transition-shadow outline-none focus-visible:ring-[3px] disabled:cursor-not-allowed disabled:opacity-50",
                "checked:after:content-[''] checked:after:block checked:after:size-3.5 checked:after:bg-primary-foreground",
                "checked:after:mask-[image:var(--check-icon)] checked:after:mask-size-[0.9rem] checked:after:mask-no-repeat checked:after:mask-center",
            ],
            _ =>
            [
                "placeholder:text-muted-foreground selection:bg-primary selection:text-primary-foreground dark:bg-input/30 border-input h-9 w-full min-w-0 rounded-md border bg-transparent px-3 py-1 text-base shadow-xs transition-[color,box-shadow] outline-none disabled:pointer-events-none disabled:cursor-not-allowed disabled:opacity-50 md:text-sm",
                "file:text-foreground file:inline-flex file:h-7 file:border-0 file:bg-transparent file:text-sm file:font-medium",
                "focus-visible:border-ring focus-visible:ring-ring/50 focus-visible:ring-[3px]",
                "aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive",
                "[&.input-validation-error]:ring-destructive/20 dark:[&.input-validation-error]:ring-destructive/40 [&.input-validation-error]:border-destructive",
            ],
        };

        output.TagName = "input";
        output.TagMode = TagMode.SelfClosing;

        output.Attributes.SetAttribute("data-slot", "input");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(classNames.Union([output.GetUserSuppliedClass()]).ToArray())
        );

        return type switch
        {
            "checkbox" => Task.FromResult(AutoFieldLayout.HorizontalInputFirst),
            _ => Task.FromResult(AutoFieldLayout.Vertical),
        };
    }
}
