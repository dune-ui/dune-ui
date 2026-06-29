using System.Globalization;
using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-toggle-group-item")]
public class ToggleGroupItemTagHelper : DuneUITagHelperBase
{
    public ToggleGroupItemTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    /// <summary>
    ///     The value this item posts when selected.
    /// </summary>
    [HtmlAttributeName("value")]
    public string? Value { get; set; }

    /// <summary>
    ///     Overrides the parent group's variant for this item.
    /// </summary>
    [HtmlAttributeName("variant")]
    public ToggleVariant? Variant { get; set; }

    /// <summary>
    ///     Overrides the parent group's size for this item.
    /// </summary>
    [HtmlAttributeName("size")]
    public ToggleSize? Size { get; set; }

    /// <summary>
    ///     Forces the selected state. When unset, selection is derived from the value(s) bound
    ///     through the parent group's <c>asp-for</c>.
    /// </summary>
    [HtmlAttributeName("selected")]
    public bool? Selected { get; set; }

    [HtmlAttributeName("disabled")]
    public bool? Disabled { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        // The parent group resolves and publishes the shared configuration; the item only
        // contributes its own value and any per-item variant/size override.
        var toggleGroupContext = GetContext<ToggleGroupContext>(context);

        var effectiveVariant = Variant ?? toggleGroupContext?.Variant ?? ToggleVariant.Default;
        var effectiveSize = Size ?? toggleGroupContext?.Size ?? ToggleSize.Default;
        var type = toggleGroupContext?.Type ?? ToggleGroupType.Single;

        // Capture the author's content (icon/text) before we repurpose the host element.
        var childContent = await output.GetChildContentAsync();
        var userClass = output.GetUserSuppliedClass();

        /*
         * Single-select uses radios (native arrow-key roving + one value); multi-select uses
         * checkboxes that share a name and post a collection. Seeding the input from the host
         * attributes routes author extras (e.g. an icon-only item's aria-label) onto the
         * focusable control. The value posts back with no JavaScript.
         */
        var inputOutput = new TagHelperOutput(
            "input",
            new TagHelperAttributeList(output.Attributes),
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
        );
        inputOutput.Attributes.SetAttribute(
            "type",
            type == ToggleGroupType.Single ? "radio" : "checkbox"
        );
        inputOutput.Attributes.SetAttribute("data-slot", "toggle-group-item-input");

        if (!string.IsNullOrEmpty(toggleGroupContext?.FieldName))
        {
            inputOutput.Attributes.SetAttribute("name", toggleGroupContext.FieldName);
        }

        if (Value != null)
        {
            inputOutput.Attributes.SetAttribute("value", Value);
        }

        if (
            Selected
            ?? (
                Value != null
                && toggleGroupContext != null
                && toggleGroupContext.IsItemSelected(Value)
            )
        )
        {
            inputOutput.Attributes.SetAttribute("checked", "checked");
        }

        if (Disabled == true)
        {
            inputOutput.Attributes.SetAttribute("disabled", "disabled");
        }

        // Visually hidden but still focusable so native keyboard selection drives has-* state.
        inputOutput.Attributes.SetAttribute("class", "peer sr-only");

        output.Attributes.Clear();
        output.TagName = "label";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("data-slot", "toggle-group-item");
        output.Attributes.SetAttribute("data-variant", effectiveVariant.GetDataAttributeText());
        output.Attributes.SetAttribute("data-size", effectiveSize.GetDataAttributeText());
        // The first/last edge-rounding variants (data-[spacing=0]:first:rounded-l-md) read
        // data-spacing off the item itself, so mirror the group's spacing onto each item.
        output.Attributes.SetAttribute(
            "data-spacing",
            (toggleGroupContext?.Spacing ?? 0).ToString(CultureInfo.InvariantCulture)
        );
        output.Attributes.SetAttribute(
            "class",
            ToggleRenderingHelper.BuildClass(
                ClassMerger,
                effectiveVariant,
                effectiveSize,
                includeGroupItemToken: true,
                userClass
            )
        );

        output.Content.AppendHtml(inputOutput);
        output.Content.AppendHtml(childContent);
    }
}
