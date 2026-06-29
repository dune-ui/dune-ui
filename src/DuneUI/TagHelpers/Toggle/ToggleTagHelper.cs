using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using FrameworkInputTagHelper = Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-toggle")]
public class ToggleTagHelper : FieldInputBaseTagHelper
{
    private readonly IHtmlGenerator _htmlGenerator;

    public ToggleTagHelper(
        ThemeManager themeManager,
        IHtmlGenerator htmlGenerator,
        ICssClassMerger classMerger
    )
        : base(themeManager, htmlGenerator, classMerger)
    {
        _htmlGenerator = htmlGenerator ?? throw new ArgumentNullException(nameof(htmlGenerator));
    }

    [HtmlAttributeName("form")]
    public string? FormName { get; set; }

    /// <summary>
    ///     The visual style of the toggle.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="ToggleVariant.Default" />.
    /// </remarks>
    [HtmlAttributeName("variant")]
    public ToggleVariant? Variant { get; set; }

    /// <summary>
    ///     The size of the toggle.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="ToggleSize.Default" />.
    /// </remarks>
    [HtmlAttributeName("size")]
    public ToggleSize? Size { get; set; }

    [HtmlAttributeName("value")]
    public string? Value { get; set; }

    protected override async Task<AutoFieldConfiguration> RenderInput(
        TagHelperContext context,
        TagHelperOutput output,
        IDictionary<string, object?>? htmlAttributes
    )
    {
        var effectiveVariant = Variant ?? ToggleVariant.Default;
        var effectiveSize = Size ?? ToggleSize.Default;

        // Capture the author's content (icon/text) before we repurpose the host element.
        var childContent = await output.GetChildContentAsync();

        /*
         * Render the hidden native checkbox that carries the on/off value. Backing the toggle
         * with a checkbox means the value posts back and model-binds like any other checkbox —
         * no JavaScript required — while the pressed visuals are driven from CSS (has-[:checked])
         * on the wrapping label.
         */
        var inputOutput = new TagHelperOutput(
            "input",
            new TagHelperAttributeList(output.Attributes),
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
        );
        if (For == null)
        {
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
            // A bound toggle binds a bool, so the framework input tag helper infers a checkbox
            // (and emits the hidden companion field so an unpressed toggle posts "false"). We
            // leave InputTypeName unset so the framework does not copy a "type" attribute that
            // <dui-toggle> never carries.
            var inputTagHelper = new FrameworkInputTagHelper(_htmlGenerator)
            {
                For = For,
                FormName = FormName,
                Value = Value,
                Name = Name,
                ViewContext = ViewContext,
            };
            inputTagHelper.Process(context, inputOutput);
        }

        inputOutput.Attributes.SetAttribute("type", "checkbox");

        if (!inputOutput.Attributes.ContainsName("data-slot"))
        {
            inputOutput.Attributes.SetAttribute("data-slot", "toggle-input");
        }

        // The input is visually hidden but stays focusable, so keyboard focus + Space toggle it
        // and the label reflects its state via the has-* variants.
        inputOutput.Attributes.SetAttribute("class", "peer sr-only");

        var userClass = output.GetUserSuppliedClass();

        // Turn the host element into the label wrapper around the input + author content.
        output.Attributes.Clear();
        output.TagName = "label";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("data-slot", "toggle");
        output.Attributes.SetAttribute("data-variant", effectiveVariant.GetDataAttributeText());
        output.Attributes.SetAttribute("data-size", effectiveSize.GetDataAttributeText());
        output.Attributes.SetAttribute(
            "class",
            ToggleRenderingHelper.BuildClass(
                ClassMerger,
                effectiveVariant,
                effectiveSize,
                includeGroupItemToken: false,
                userClass
            )
        );

        output.Content.AppendHtml(inputOutput);
        output.Content.AppendHtml(childContent);

        return new AutoFieldConfiguration(AutoFieldLayout.Vertical);
    }
}
