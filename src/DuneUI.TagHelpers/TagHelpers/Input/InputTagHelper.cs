using DuneUI.Icons;
using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using FrameworkInputTagHelper = Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-input", TagStructure = TagStructure.WithoutEndTag)]
public class InputTagHelper : FieldInputBaseTagHelper
{
    private readonly IHtmlGenerator _htmlGenerator;
    private readonly IIconManager _iconManager;

    public InputTagHelper(
        ThemeManager themeManager,
        IHtmlGenerator htmlGenerator,
        ICssClassMerger classMerger,
        IIconManager iconManager
    )
        : base(themeManager, htmlGenerator, classMerger)
    {
        _htmlGenerator = htmlGenerator ?? throw new ArgumentNullException(nameof(htmlGenerator));
        _iconManager = iconManager;
    }

    [HtmlAttributeName("form")]
    public string? FormName { get; set; }

    [HtmlAttributeName("asp-format")]
    public string? Format { get; set; }

    [HtmlAttributeName("type")]
    public string? InputTypeName { get; set; }

    [HtmlAttributeName("value")]
    public string? Value { get; set; }

    /*protected override Task<AutoFieldLayout> RenderInput(
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
            var inputTagHelper = new FrameworkInputTagHelper(_htmlGenerator)
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
            ? typeAttribute.Value?.ToString()
            : null;

        ClassElement[] classNames = type switch
        {
            "checkbox" =>
            [
                "bg-center bg-no-repeat !bg-current ![background-size:.85em_.85em]",
                "checked:bg-no-repeat appearance-none bg-(image:--check-icon)",
                // "appearance-none border-input dark:bg-input/30 checked:bg-primary dark:checked:bg-primary checked:border-primary focus-visible:border-ring focus-visible:ring-ring/50 aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive size-4 shrink-0 rounded-[4px] border shadow-xs transition-shadow outline-none focus-visible:ring-[3px] disabled:cursor-not-allowed disabled:opacity-50",
                // "checked:after:content-[''] checked:after:block checked:after:size-3.5 checked:after:bg-primary-foreground",
                // "checked:after:mask-[image:var(--check-icon)] checked:after:mask-size-[0.9rem] checked:after:mask-no-repeat checked:after:mask-center",
            ],
            "radio" =>
            [
                // "appearance-none border-input text-primary focus-visible:border-ring focus-visible:ring-ring/50 aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive dark:bg-input/30 aspect-square size-4 shrink-0 rounded-full border shadow-xs transition-[color,box-shadow] outline-none focus-visible:ring-[3px] disabled:cursor-not-allowed disabled:opacity-50 relative",
                // "checked:before:absolute checked:before:top-1/2 checked:before:left-1/2 checked:before:-translate-x-1/2 checked:before:-translate-y-1/2 checked:before:content-[''] checked:before:rounded-full checked:before:size-2 checked:before:bg-primary",
            ],
            _ =>
            [
                // "placeholder:text-muted-foreground selection:bg-primary selection:text-primary-foreground dark:bg-input/30 border-input h-9 w-full min-w-0 rounded-md border bg-transparent px-3 py-1 text-base shadow-xs transition-[color,box-shadow] outline-none disabled:pointer-events-none disabled:cursor-not-allowed disabled:opacity-50 md:text-sm",
                // "file:text-foreground file:inline-flex file:h-7 file:border-0 file:bg-transparent file:text-sm file:font-medium",
                // "focus-visible:border-ring focus-visible:ring-ring/50 focus-visible:ring-[3px]",
                // "aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive",
                new ComponentName("dui-input"),
                "file:text-foreground placeholder:text-muted-foreground w-full min-w-0 outline-none file:inline-flex file:border-0 file:bg-transparent disabled:pointer-events-none disabled:cursor-not-allowed disabled:opacity-50",
                // Custom DuneUI overrides for validation
                "[&.input-validation-error]:ring-destructive/20 dark:[&.input-validation-error]:ring-destructive/40 [&.input-validation-error]:border-destructive",
            ],
        };

        output.TagName = "input";
        output.TagMode = TagMode.SelfClosing;

        if (!output.Attributes.ContainsName("data-slot"))
        {
            output.Attributes.SetAttribute("data-slot", "input");
        }
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                classNames.Union([new ClassList(output.GetUserSuppliedClass())]).ToArray()
            )
        );

        // Add input-specific processing
        switch (type)
        {
            case "checkbox":
                output.Attributes.SetAttribute("role", "checkbox");
                break;
            case "radio":
                output.Attributes.SetAttribute("role", "radio");
                // if (GetParentTagHelper<RadioGroupTagHelper>() != null)
                // {
                //     output.Attributes.SetAttribute("data-slot", "radio-group-item");
                // }
                break;
        }

        return type switch
        {
            "checkbox" or "radio" => Task.FromResult(AutoFieldLayout.HorizontalInputFirst),
            _ => Task.FromResult(AutoFieldLayout.Vertical),
        };
    }*/
    protected override async Task<AutoFieldLayout> RenderInput(
        TagHelperContext context,
        TagHelperOutput output,
        IDictionary<string, object?>? htmlAttributes
    )
    {
        /*
         * Render the select
         */
        var inputOutput = new TagHelperOutput(
            "input",
            new TagHelperAttributeList(output.Attributes),
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
        );
        if (For == null)
        {
            if (InputTypeName != null)
            {
                inputOutput.CopyHtmlAttribute("type", context);
            }

            if (Value != null)
            {
                inputOutput.CopyHtmlAttribute("value", context);
            }

            if (FormName != null)
            {
                inputOutput.CopyHtmlAttribute("form", context);
            }
        }
        else
        {
            var inputTagHelper = new FrameworkInputTagHelper(_htmlGenerator)
            {
                For = For,
                Format = Format,
                FormName = FormName,
                InputTypeName = InputTypeName,
                Value = Value,
                Name = Name,
                ViewContext = ViewContext,
            };
            inputTagHelper.Process(context, inputOutput);
        }

        var type = inputOutput.Attributes.TryGetAttribute("type", out var typeAttribute)
            ? typeAttribute.Value?.ToString()
            : null;

        ClassElement[] classNames = type switch
        {
            "checkbox" =>
            [
                new ComponentName("dui-checkbox"),
                "peer relative shrink-0 outline-none after:absolute after:-inset-x-3 after:-inset-y-2 disabled:cursor-not-allowed disabled:opacity-50",
                // Custom DuneUI override
                "before:content[''] peer relative appearance-none",
            ],
            "radio" =>
            [
                // "appearance-none border-input text-primary focus-visible:border-ring focus-visible:ring-ring/50 aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive dark:bg-input/30 aspect-square size-4 shrink-0 rounded-full border shadow-xs transition-[color,box-shadow] outline-none focus-visible:ring-[3px] disabled:cursor-not-allowed disabled:opacity-50 relative",
                // "checked:before:absolute checked:before:top-1/2 checked:before:left-1/2 checked:before:-translate-x-1/2 checked:before:-translate-y-1/2 checked:before:content-[''] checked:before:rounded-full checked:before:size-2 checked:before:bg-primary",
            ],
            _ =>
            [
                new ComponentName("dui-input"),
                "file:text-foreground placeholder:text-muted-foreground w-full min-w-0 outline-none file:inline-flex file:border-0 file:bg-transparent disabled:pointer-events-none disabled:cursor-not-allowed disabled:opacity-50",
                // Custom DuneUI overrides for validation
                "[&.input-validation-error]:ring-destructive/20 dark:[&.input-validation-error]:ring-destructive/40 [&.input-validation-error]:border-destructive",
            ],
        };

        if (!inputOutput.Attributes.ContainsName("data-slot"))
        {
            inputOutput.Attributes.SetAttribute("data-slot", "input");
        }
        inputOutput.Attributes.SetAttribute(
            "class",
            // "before:content[''] peer relative appearance-none border-input dark:bg-input/30 checked:bg-primary checked:text-primary-foreground dark:checked:bg-primary checked:border-primary aria-invalid:aria-checked:border-primary [&.input-validation-error]:aria-checked:border-primary aria-invalid:border-destructive [&.input-validation-error]:border-destructive dark:aria-invalid:border-destructive/50 dark:[&.input-validation-error]:border-destructive/50 focus-visible:border-ring focus-visible:ring-ring/50 aria-invalid:ring-destructive/20 [&.input-validation-error]:ring-destructive/20 dark:aria-invalid:ring-destructive/40 dark:[&.input-validation-error]:ring-destructive/40 flex size-4 items-center justify-center rounded-[4px] border shadow-xs transition-shadow group-has-disabled/field:opacity-50 focus-visible:ring-[3px] peer  shrink-0 outline-none after:absolute after:-inset-x-3 after:-inset-y-2 disabled:cursor-not-allowed disabled:opacity-50"
            ClassMerger.Merge(
                classNames
                    .Union([inputOutput.GetUserSuppliedClass(), output.GetUserSuppliedClass()])
                    .ToArray()
            )
        );

        output.Content.AppendHtml(inputOutput);

        output.Attributes.Clear();
        switch (type)
        {
            case "checkbox":
                // Wrap it in a span
                output.TagName = "span";
                output.TagMode = TagMode.StartTagAndEndTag;

                output.Attributes.SetAttribute("class", "relative flex items-center");

                // Add the span
                var spanTagBuilder = new TagBuilder("span");
                spanTagBuilder.Attributes.Add(
                    "class",
                    ClassMerger.Merge(
                        new ComponentName("dui-checkbox-indicator"),
                        "invisible peer-checked:visible"
                    )
                );

                // Add the icon
                var iconOutput = new TagHelperOutput(
                    "svg",
                    [
                        new TagHelperAttribute(
                            "class",
                            "pointer-events-none absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 text-neutral-100 dark:text-black"
                        ),
                    ],
                    (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
                );
                var iconTagHelper = new IconTagHelper(ThemeManager, ClassMerger, _iconManager)
                {
                    Name = "check",
                };
                await iconTagHelper.ProcessAsync(context, iconOutput);
                spanTagBuilder.InnerHtml.AppendHtml(iconOutput);

                output.Content.AppendHtml(spanTagBuilder);
                break;
            default:
                output.TagName = null;
                break;
        }

        return type switch
        {
            "checkbox" or "radio" => AutoFieldLayout.HorizontalInputFirst,
            _ => AutoFieldLayout.Vertical,
        };
    }
}
