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

    protected override async Task<AutoFieldConfiguration> RenderInput(
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
                "before:content[''] appearance-none",
            ],
            "radio" =>
            [
                new ComponentName("dui-radiobutton"),
                "peer relative aspect-square shrink-0 border outline-none after:absolute after:-inset-x-3 after:-inset-y-2 disabled:cursor-not-allowed disabled:opacity-50",
                // Custom DuneUI override
                "before:content[''] appearance-none rounded-full",
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
                var checkboxSpan = new TagBuilder("span");
                checkboxSpan.Attributes.Add(
                    "class",
                    ClassMerger.Merge(
                        new ComponentName("dui-checkbox-indicator"),
                        "invisible peer-checked:visible"
                    )
                );

                // Add the icon
                var checkboxIconOutput = new TagHelperOutput(
                    "svg",
                    [
                        new TagHelperAttribute(
                            "class",
                            "pointer-events-none absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 text-neutral-100 dark:text-black"
                        ),
                    ],
                    (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
                );
                var checkboxIconTagHelper = new IconTagHelper(
                    ThemeManager,
                    ClassMerger,
                    _iconManager
                )
                {
                    Name = "check",
                };
                await checkboxIconTagHelper.ProcessAsync(context, checkboxIconOutput);
                checkboxSpan.InnerHtml.AppendHtml(checkboxIconOutput);

                output.Content.AppendHtml(checkboxSpan);
                break;
            case "radio":
                // Wrap it in a span
                output.TagName = "span";
                output.TagMode = TagMode.StartTagAndEndTag;

                output.Attributes.SetAttribute("class", "relative flex items-center");

                // Add the span
                var spanTagBuilder = new TagBuilder("span");
                spanTagBuilder.Attributes.Add(
                    "class",
                    ClassMerger.Merge(
                        new ComponentName("dui-radiobutton-indicator"),
                        "size-0",
                        "invisible peer-checked:visible"
                    )
                );

                // Add the icon
                var iconOutput = new TagHelperOutput(
                    "svg",
                    [
                        new TagHelperAttribute(
                            "class",
                            ClassMerger.Merge(
                                new ComponentName("dui-radiobutton-indicator-icon"),
                                "pointer-events-none"
                            )
                        ),
                    ],
                    (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
                );
                var iconTagHelper = new IconTagHelper(ThemeManager, ClassMerger, _iconManager)
                {
                    Name = "circle",
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
            // We do not automatically display the error label for radio buttons,
            // since you typically do not want to have the error displayed on each
            // individual radio button. Rather, the error will be displayed for the
            // group of radio buttons as a whole. As such, the developer will need
            // to add the dui-field-error Tag Helper explicitly to their form.
            "radio" when For != null => new AutoFieldConfiguration(
                AutoFieldLayout.HorizontalInputFirst,
                AutoFieldElement.Label | AutoFieldElement.Description
            ),
            "checkbox" or "radio" => new AutoFieldConfiguration(
                AutoFieldLayout.HorizontalInputFirst
            ),
            _ => new AutoFieldConfiguration(AutoFieldLayout.Vertical),
        };
    }
}
