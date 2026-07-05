using System.Globalization;
using DuneUI.Icons;
using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using FrameworkInputTagHelper = Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper;

namespace DuneUI.TagHelpers;

/// <summary>
///     A segmented one-time-code input. A single real <c>&lt;input&gt;</c> holds the whole code
///     and posts it as one form value;
///     the presentational slot cells (one per character) display the value and the active caret.
///     The <c>del-input-otp</c> web component distributes the value into the cells and drives the
///     active / caret state once hydrated.
/// </summary>
/// <remarks>
///     Slots can be auto-generated from <c>max-length</c> (optionally split into groups via
///     <c>groups</c>), or composed by hand with <c>dui-input-otp-group</c>,
///     <c>dui-input-otp-slot</c>, and <c>dui-input-otp-separator</c> children.
/// </remarks>
[HtmlTargetElement("dui-input-otp")]
public class InputOtpTagHelper : FieldInputBaseTagHelper
{
    private readonly IHtmlGenerator _htmlGenerator;
    private readonly IIconManager _iconManager;

    public InputOtpTagHelper(
        ThemeManager themeManager,
        IHtmlGenerator htmlGenerator,
        ICssClassMerger classMerger,
        IIconManager iconManager
    )
        : base(themeManager, htmlGenerator, classMerger)
    {
        _htmlGenerator = htmlGenerator ?? throw new ArgumentNullException(nameof(htmlGenerator));
        _iconManager = iconManager ?? throw new ArgumentNullException(nameof(iconManager));
    }

    /// <summary>
    ///     The number of slots. Defaults to 6 (or the sum of <see cref="Groups" /> when set).
    /// </summary>
    [HtmlAttributeName("max-length")]
    public int? MaxLength { get; set; }

    /// <summary>
    ///     A regular expression the input is constrained to (characters that would make the value
    ///     fail to match are rejected). Defaults to digits only (<c>^\d*$</c>).
    /// </summary>
    [HtmlAttributeName("pattern")]
    public string? Pattern { get; set; }

    /// <summary>
    ///     Splits the slots into groups (separated by a separator), e.g. <c>"3,3"</c>. When omitted
    ///     all slots render in a single group. The sizes must sum to <see cref="MaxLength" /> when
    ///     both are set.
    /// </summary>
    [HtmlAttributeName("groups")]
    public string? Groups { get; set; }

    /// <summary>
    ///     The virtual keyboard hint applied to the input. Defaults to <c>numeric</c>.
    /// </summary>
    [HtmlAttributeName("inputmode")]
    public string? InputMode { get; set; }

    /// <summary>
    ///     Whether the input is disabled.
    /// </summary>
    /// <remarks>
    ///     Defaults to <c>false</c>.
    /// </remarks>
    [HtmlAttributeName("disabled")]
    public bool? Disabled { get; set; }

    /// <summary>
    ///     Forces the invalid (error) styling. Set to <c>true</c> for manual validation when not
    ///     model binding; otherwise the invalid state is derived from the bound field's ModelState.
    /// </summary>
    [HtmlAttributeName("aria-invalid")]
    public bool? AriaInvalid { get; set; }

    /// <summary>
    ///     The initial code when not bound via <c>asp-for</c>.
    /// </summary>
    [HtmlAttributeName("value")]
    public string? Value { get; set; }

    /// <summary>
    ///     The id of the form the input is associated with.
    /// </summary>
    [HtmlAttributeName("form")]
    public string? FormName { get; set; }

    protected override async Task<AutoFieldConfiguration> RenderInput(
        TagHelperContext context,
        TagHelperOutput output,
        IDictionary<string, object?>? htmlAttributes
    )
    {
        var groupSizes = ResolveGroupSizes(out var maxLength);
        var effectivePattern = string.IsNullOrEmpty(Pattern) ? @"^\d*$" : Pattern;
        var effectiveInputMode = string.IsNullOrEmpty(InputMode) ? "numeric" : InputMode;
        var effectiveDisabled = Disabled ?? false;

        var code = For?.Model?.ToString() ?? Value ?? string.Empty;
        var fieldName = ResolveName();
        var hasError = AriaInvalid == true || HasModelError(fieldName);
        var userClass = output.GetUserSuppliedClass();

        // The single real input: it holds the whole code and posts it as one value. We overlay it
        // transparently over the slots so a click anywhere focuses it.
        var inputOutput = RenderUnderlyingInput(
            context,
            fieldName,
            maxLength,
            effectiveInputMode,
            hasError
        );

        // The host becomes <del-del-input-otp>, the flex container. Drop the name the base copied onto
        // the host: the value posts through the real input, not the host element.
        output.Attributes.RemoveAll("name");
        output.TagName = "del-input-otp";
        output.TagMode = TagMode.StartTagAndEndTag;

        // Mark the container with data-input-otp-container and put data-slot="input-otp" on the
        // real input instead.
        output.Attributes.SetAttribute("data-input-otp-container", "true");
        output.Attributes.SetAttribute(
            "maxlength",
            maxLength.ToString(CultureInfo.InvariantCulture)
        );
        output.Attributes.SetAttribute("data-pattern", effectivePattern);
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-input-otp"),
                "flex items-center has-disabled:opacity-50",
                userClass
            )
        );
        // Inline container styles: the
        // positioning context for the absolutely-overlaid input, the text-field affordances, and
        // --root-height, which the input's font-size keys off so the transparent text lines up with
        // the slots. The 32px fallback matches the universal size-8 slot; the del-input-otp web
        // component re-measures and overrides it on hydration for custom slot sizes.
        output.Attributes.SetAttribute(
            "style",
            "position: relative; cursor: text; user-select: none; pointer-events: none; "
                + "--root-height: 32px;"
        );

        // Resolve the fake-caret classes server-side (theme token + cross-theme statics) and hand
        // them to the web component, which builds the caret element on hydration and can't resolve
        // themepack tokens itself.
        output.Attributes.SetAttribute(
            "data-caret-class",
            ClassMerger.Merge(
                new ThemeToken("dui-input-otp-caret"),
                "pointer-events-none absolute inset-0 flex items-center justify-center"
            )
        );
        output.Attributes.SetAttribute(
            "data-caret-line-class",
            ClassMerger.Merge(new ThemeToken("dui-input-otp-caret-line"))
        );

        // Publish the shared state before rendering children so authored slots can read it.
        SetContext(
            context,
            new InputOtpContext
            {
                Code = code,
                MaxLength = maxLength,
                Disabled = effectiveDisabled,
                HasError = hasError,
            }
        );

        var childContent = await output.GetChildContentAsync();
        if (!childContent.IsEmptyOrWhiteSpace)
        {
            // Author composed groups/slots/separators by hand.
            output.Content.AppendHtml(childContent);
        }
        else
        {
            await AppendAutoGeneratedGroupsAsync(context, output, groupSizes, code, hasError);
        }

        // Render the field last, inside an absolute overlay wrapper (matching guilhermerodz), so it
        // covers the slots and receives pointer events ahead of them. The wrapper itself is
        // pointer-events:none; the input re-enables pointer-events:all in its own inline style.
        var inputWrapper = new TagBuilder("div");
        inputWrapper.Attributes.Add("style", "position:absolute;inset:0;pointer-events:none");
        inputWrapper.InnerHtml.AppendHtml(inputOutput);
        output.Content.AppendHtml(inputWrapper);

        return new AutoFieldConfiguration(AutoFieldLayout.Vertical);
    }

    private TagHelperOutput RenderUnderlyingInput(
        TagHelperContext context,
        string? fieldName,
        int maxLength,
        string inputMode,
        bool hasError
    )
    {
        var inputOutput = new TagHelperOutput(
            "input",
            [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
        )
        {
            TagMode = TagMode.StartTagOnly,
        };

        if (For == null)
        {
            inputOutput.Attributes.SetAttribute("type", "text");
            if (Value != null)
            {
                inputOutput.Attributes.SetAttribute("value", Value);
            }
            if (fieldName != null)
            {
                inputOutput.Attributes.SetAttribute("name", fieldName);
            }
            if (FormName != null)
            {
                inputOutput.Attributes.SetAttribute("form", FormName);
            }
        }
        else
        {
            // Defer to the framework input helper for the name, the bound value, and the
            // unobtrusive-validation attributes (incl. the .input-validation-error class on error).
            // Leave InputTypeName unset: the framework infers "text" from the string model.
            // Setting it would make the helper copy a non-existent "type" attribute off the host.
            var frameworkInput = new FrameworkInputTagHelper(_htmlGenerator)
            {
                For = For,
                FormName = FormName,
                Value = Value,
                Name = Name,
                ViewContext = ViewContext,
            };
            frameworkInput.Process(context, inputOutput);
            inputOutput.Attributes.SetAttribute("type", "text");
        }

        inputOutput.Attributes.SetAttribute("data-slot", "input-otp");
        inputOutput.Attributes.SetAttribute(
            "maxlength",
            maxLength.ToString(CultureInfo.InvariantCulture)
        );
        inputOutput.Attributes.SetAttribute("inputmode", inputMode);
        inputOutput.Attributes.SetAttribute("autocomplete", "one-time-code");
        inputOutput.Attributes.SetAttribute("autocorrect", "off");
        inputOutput.Attributes.SetAttribute("spellcheck", "false");
        // Carry the theme token (cn-input-otp-input) + the cross-theme disabled cursor, folding in
        // any class the framework input helper added (e.g. .input-validation-error on error).
        inputOutput.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-input-otp-input"),
                "disabled:cursor-not-allowed",
                inputOutput.Attributes["class"]?.Value?.ToString()
            )
        );
        // Drive the group's has-aria-invalid styling from the server-rendered validation state.
        if (hasError)
        {
            inputOutput.Attributes.SetAttribute("aria-invalid", "true");
        }
        if (Disabled == true)
        {
            inputOutput.Attributes.SetAttribute("disabled", "disabled");
        }
        // Inline styles (not utility classes) so the overlay is deterministic regardless of which
        // utilities Tailwind generated. The text and caret are transparent because the slots draw
        // the value and the fake caret; the field exists only to capture native input. The
        // ::selection highlight (which inline styles can't reach) is hidden by the web component.
        inputOutput.Attributes.SetAttribute(
            "style",
            "position: absolute; inset: 0px; width: calc(100% + 40px); height: 100%; display: flex; "
                + "text-align: left; opacity: 1; color: transparent; pointer-events: all; "
                + "background: transparent; caret-color: transparent; border: 0px solid transparent; "
                + "outline: transparent solid 0px; box-shadow: none; line-height: 1; "
                + "letter-spacing: -0.5em; font-size: var(--root-height); font-family: monospace; "
                + "font-variant-numeric: tabular-nums; clip-path: inset(0px 40px 0px 0px);"
        );

        return inputOutput;
    }

    private async Task AppendAutoGeneratedGroupsAsync(
        TagHelperContext context,
        TagHelperOutput output,
        IReadOnlyList<int> groupSizes,
        string code,
        bool hasError
    )
    {
        var index = 0;
        for (var groupIndex = 0; groupIndex < groupSizes.Count; groupIndex++)
        {
            var group = new TagBuilder("div");
            group.Attributes.Add("data-slot", "input-otp-group");
            group.Attributes.Add("class", InputOtpRenderer.GroupClass(ClassMerger, null));

            for (var slot = 0; slot < groupSizes[groupIndex]; slot++)
            {
                var character = index < code.Length ? code[index].ToString() : null;
                group.InnerHtml.AppendHtml(
                    InputOtpRenderer.BuildSlot(ClassMerger, index, character, hasError)
                );
                index++;
            }

            output.Content.AppendHtml(group);

            if (groupIndex < groupSizes.Count - 1)
            {
                var separator = new TagHelperOutput(
                    "div",
                    [],
                    (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
                )
                {
                    TagMode = TagMode.StartTagAndEndTag,
                };
                separator.Attributes.SetAttribute("data-slot", "input-otp-separator");
                separator.Attributes.SetAttribute("role", "separator");
                separator.Attributes.SetAttribute(
                    "class",
                    InputOtpRenderer.SeparatorClass(ClassMerger, null)
                );
                await InputOtpRenderer.RenderDefaultSeparatorContentAsync(
                    separator.Content,
                    context,
                    ThemeManager,
                    ClassMerger,
                    _iconManager
                );
                output.Content.AppendHtml(separator);
            }
        }
    }

    /// <summary>
    ///     Resolves the per-group slot counts and the total <paramref name="maxLength" />: from
    ///     <see cref="Groups" /> when set, otherwise a single group of <see cref="MaxLength" />
    ///     (defaulting to 6).
    /// </summary>
    private IReadOnlyList<int> ResolveGroupSizes(out int maxLength)
    {
        if (!string.IsNullOrWhiteSpace(Groups))
        {
            var sizes = Groups
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(size => int.Parse(size, CultureInfo.InvariantCulture))
                .ToList();

            if (sizes.Count == 0 || sizes.Any(size => size <= 0))
            {
                throw new ArgumentException(
                    "The 'groups' attribute must be a comma-separated list of positive integers.",
                    nameof(Groups)
                );
            }

            var sum = sizes.Sum();
            if (MaxLength.HasValue && MaxLength.Value != sum)
            {
                throw new ArgumentException(
                    "The 'groups' sizes must sum to 'max-length'.",
                    nameof(Groups)
                );
            }

            maxLength = sum;
            return sizes;
        }

        maxLength = MaxLength ?? 6;
        if (maxLength <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(MaxLength),
                "Max length must be positive."
            );
        }

        return [maxLength];
    }

    /// <summary>
    ///     Resolves the <c>name</c> applied to the posted input: the explicit
    ///     <see cref="FieldInputBaseTagHelper.Name" /> when supplied, otherwise the fully qualified
    ///     field name from the bound expression (respecting any <c>HtmlFieldPrefix</c>).
    /// </summary>
    private string? ResolveName()
    {
        if (!string.IsNullOrEmpty(Name))
        {
            return Name;
        }

        if (!string.IsNullOrEmpty(For?.Name))
        {
            return ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(For.Name);
        }

        return null;
    }

    private bool HasModelError(string? fieldName)
    {
        var modelState = ViewContext.ViewData.ModelState;

        if (
            !string.IsNullOrEmpty(For?.Name)
            && modelState.TryGetValue(For.Name, out var forEntry)
            && forEntry.Errors.Count > 0
        )
        {
            return true;
        }

        return !string.IsNullOrEmpty(fieldName)
            && modelState.TryGetValue(fieldName, out var nameEntry)
            && nameEntry.Errors.Count > 0;
    }
}
