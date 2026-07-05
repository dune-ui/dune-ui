using System.Collections;
using System.Globalization;
using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     An input for selecting a numeric value, or a range of values, by dragging one or more
///     thumbs along a track.
/// </summary>
[HtmlTargetElement("dui-slider", TagStructure = TagStructure.WithoutEndTag)]
public class SliderTagHelper : FieldInputBaseTagHelper
{
    public SliderTagHelper(
        ThemeManager themeManager,
        IHtmlGenerator htmlGenerator,
        ICssClassMerger classMerger
    )
        : base(themeManager, htmlGenerator, classMerger) { }

    /// <summary>
    ///     The minimum value the slider can take.
    /// </summary>
    [HtmlAttributeName("min")]
    public int? Min { get; set; }

    /// <summary>
    ///     The maximum value the slider can take.
    /// </summary>
    [HtmlAttributeName("max")]
    public int? Max { get; set; }

    /// <summary>
    ///     The increment between selectable values.
    /// </summary>
    [HtmlAttributeName("step")]
    public int? Step { get; set; }

    /// <summary>
    ///     The minimum distance (in value units) enforced between adjacent thumbs on a range slider.
    /// </summary>
    [HtmlAttributeName("min-distance")]
    public int? MinDistance { get; set; }

    /// <summary>
    ///     The orientation of the slider.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="SliderOrientation.Horizontal" />.
    /// </remarks>
    [HtmlAttributeName("orientation")]
    public SliderOrientation? Orientation { get; set; }

    /// <summary>
    ///     How the thumb is positioned relative to its value: <see cref="SliderThumbAlignment.Edge" />
    ///     keeps the thumb fully within the track at the extremes (the default),
    ///     <see cref="SliderThumbAlignment.Center" /> centers it on the value so it overhangs the ends.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="SliderThumbAlignment.Edge" />.
    /// </remarks>
    [HtmlAttributeName("thumb-alignment")]
    public SliderThumbAlignment? ThumbAlignment { get; set; }

    /// <summary>
    ///     Whether the slider is disabled and cannot be interacted with.
    /// </summary>
    /// <remarks>
    ///     Defaults to <c>false</c>.
    /// </remarks>
    [HtmlAttributeName("disabled")]
    public bool? Disabled { get; set; }

    /// <summary>
    ///     The value(s) of the slider when not bound via <c>asp-for</c>. A single number renders one
    ///     thumb; a comma-separated list (e.g. <c>"20,80"</c>) renders a thumb per value (range slider).
    /// </summary>
    [HtmlAttributeName("value")]
    public string? Value { get; set; }

    /// <summary>
    ///     The <c>id</c> of the form the slider's posted value belongs to, for associating it with a
    ///     form it is not nested within.
    /// </summary>
    [HtmlAttributeName("form")]
    public string? FormName { get; set; }

    protected override Task<AutoFieldConfiguration> RenderInput(
        TagHelperContext context,
        TagHelperOutput output,
        IDictionary<string, object?>? htmlAttributes
    )
    {
        var effectiveMin = Min ?? 0;
        var effectiveMax = Max ?? 100;
        var effectiveStep = Step ?? 1;
        var effectiveMinDistance = MinDistance ?? 0;
        var effectiveOrientation = Orientation ?? SliderOrientation.Horizontal;
        var effectiveThumbAlignment = ThumbAlignment ?? SliderThumbAlignment.Edge;
        var effectiveDisabled = Disabled ?? false;

        if (effectiveMin >= effectiveMax)
        {
            throw new ArgumentOutOfRangeException(nameof(Min), "Min must be less than Max");
        }

        var values = ResolveValues(effectiveMin);
        foreach (var value in values)
        {
            if (value < effectiveMin || value > effectiveMax)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(Value),
                    "Each slider value must be between Min and Max"
                );
            }
        }

        var orientationText = effectiveOrientation.GetDataAttributeText();
        var inputName = ResolveName();
        var userClass = output.GetUserSuppliedClass();

        // The host becomes <del-slider>, which is itself the "slider" root. Drop the
        // name attribute the base copied onto the host: the value posts through the hidden
        // inputs we render per thumb, not through the host element.
        output.Attributes.RemoveAll("name");
        output.TagName = "del-slider";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("data-slot", "slider");
        output.Attributes.SetAttribute("data-orientation", orientationText);
        output.Attributes.SetAttribute("min", effectiveMin.ToString(CultureInfo.InvariantCulture));
        output.Attributes.SetAttribute("max", effectiveMax.ToString(CultureInfo.InvariantCulture));
        output.Attributes.SetAttribute(
            "step",
            effectiveStep.ToString(CultureInfo.InvariantCulture)
        );
        output.Attributes.SetAttribute(
            "data-min-distance",
            effectiveMinDistance.ToString(CultureInfo.InvariantCulture)
        );
        output.Attributes.SetAttribute(
            "data-thumb-alignment",
            effectiveThumbAlignment.GetDataAttributeText()
        );
        if (effectiveDisabled)
        {
            output.Attributes.SetAttribute("data-disabled", "true");
        }
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-slider"),
                "relative flex w-full touch-none items-center select-none data-disabled:opacity-50 data-vertical:h-full data-vertical:w-auto data-vertical:flex-col",
                userClass
            )
        );

        // Track + filled range. For a single thumb the range fills from the start; for a range
        // slider it spans between the lowest and highest thumb.
        var lowPercent = values.Count > 1 ? Percent(values.Min(), effectiveMin, effectiveMax) : 0d;
        var highPercent = Percent(values.Max(), effectiveMin, effectiveMax);

        var track = new TagBuilder("span");
        track.Attributes.Add("data-slot", "slider-track");
        track.Attributes.Add("data-orientation", orientationText);
        track.Attributes.Add(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-slider-track"),
                "relative grow overflow-hidden select-none"
            )
        );

        var range = new TagBuilder("span");
        range.Attributes.Add("data-slot", "slider-range");
        range.Attributes.Add("data-orientation", orientationText);
        range.Attributes.Add(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-slider-range"),
                "absolute select-none data-horizontal:h-full data-vertical:w-full"
            )
        );
        range.Attributes.Add("style", RangeStyle(effectiveOrientation, lowPercent, highPercent));
        track.InnerHtml.AppendHtml(range);
        output.Content.AppendHtml(track);

        // One thumb (+ a hidden input so it posts) per value.
        for (var index = 0; index < values.Count; index++)
        {
            var value = values[index];

            var thumb = new TagBuilder("span");
            thumb.Attributes.Add("data-slot", "slider-thumb");
            thumb.Attributes.Add("data-orientation", orientationText);
            thumb.Attributes.Add("role", "slider");
            thumb.Attributes.Add("tabindex", effectiveDisabled ? "-1" : "0");
            thumb.Attributes.Add("aria-orientation", orientationText);
            thumb.Attributes.Add(
                "aria-valuemin",
                effectiveMin.ToString(CultureInfo.InvariantCulture)
            );
            thumb.Attributes.Add(
                "aria-valuemax",
                effectiveMax.ToString(CultureInfo.InvariantCulture)
            );
            thumb.Attributes.Add("aria-valuenow", value.ToString(CultureInfo.InvariantCulture));
            if (effectiveDisabled)
            {
                // Mark the thumb for assistive tech, but don't stamp data-disabled here: the
                // host carries it and applies the single opacity-50 dim for the whole subtree.
                // A second data-disabled:opacity-50 on the thumb would double-dim it (the white
                // fill drops to ~25% and the track bleeds through), and data-disabled:pointer-
                // events-none would kill the token's hover:ring-4. Disabled state is kept on the
                // host wrapper only, so per-thumb disabled utilities are left inert here.
                thumb.Attributes.Add("aria-disabled", "true");
            }
            thumb.Attributes.Add(
                "class",
                ClassMerger.Merge(
                    new ThemeToken("dui-slider-thumb"),
                    "absolute block shrink-0 select-none"
                )
            );
            thumb.Attributes.Add(
                "style",
                ThumbStyle(
                    effectiveOrientation,
                    effectiveThumbAlignment,
                    Percent(value, effectiveMin, effectiveMax)
                )
            );
            output.Content.AppendHtml(thumb);

            if (inputName != null)
            {
                var hidden = new TagBuilder("input") { TagRenderMode = TagRenderMode.SelfClosing };
                hidden.Attributes.Add("type", "hidden");
                hidden.Attributes.Add("data-slot", "slider-input");
                hidden.Attributes.Add("name", inputName);
                hidden.Attributes.Add("value", value.ToString(CultureInfo.InvariantCulture));
                if (FormName != null)
                {
                    hidden.Attributes.Add("form", FormName);
                }
                output.Content.AppendHtml(hidden);
            }
        }

        return Task.FromResult(new AutoFieldConfiguration(AutoFieldLayout.Vertical));
    }

    /// <summary>
    ///     Resolves the current thumb values: from the bound model when <c>asp-for</c> is set
    ///     (scalar or collection), otherwise from the comma-separated <see cref="Value" />,
    ///     otherwise a single thumb at <paramref name="min" />.
    /// </summary>
    private IReadOnlyList<int> ResolveValues(int min)
    {
        if (For?.Model is { } model and not string)
        {
            if (model is IEnumerable enumerable)
            {
                var fromModel = new List<int>();
                foreach (var item in enumerable)
                {
                    if (item != null)
                    {
                        fromModel.Add(Convert.ToInt32(item, CultureInfo.InvariantCulture));
                    }
                }

                if (fromModel.Count > 0)
                {
                    return fromModel;
                }
            }
            else
            {
                return [Convert.ToInt32(model, CultureInfo.InvariantCulture)];
            }
        }

        if (!string.IsNullOrWhiteSpace(Value))
        {
            return Value
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(v => int.Parse(v, CultureInfo.InvariantCulture))
                .ToList();
        }

        return [min];
    }

    /// <summary>
    ///     Resolves the <c>name</c> applied to the posted hidden input(s): the explicit
    ///     <see cref="FieldInputBaseTagHelper.Name" /> when supplied, otherwise the fully
    ///     qualified field name from the bound expression (respecting any <c>HtmlFieldPrefix</c>).
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

    private static double Percent(int value, int min, int max) =>
        (double)(value - min) / (max - min) * 100d;

    private static string FormatPercent(double percent) =>
        percent.ToString("0.####", CultureInfo.InvariantCulture);

    private static string RangeStyle(SliderOrientation orientation, double low, double high) =>
        orientation == SliderOrientation.Vertical
            ? $"bottom: {FormatPercent(low)}%; top: {FormatPercent(100d - high)}%;"
            : $"left: {FormatPercent(low)}%; right: {FormatPercent(100d - high)}%;";

    private static string ThumbStyle(
        SliderOrientation orientation,
        SliderThumbAlignment alignment,
        double percent
    )
    {
        // Edge alignment shifts the thumb by its value percentage (relative to its own width),
        // so its leading/trailing edge stays flush with the track ends instead of overhanging;
        // center alignment shifts by a constant 50%. The percentage translate means we never
        // need to know the thumb's pixel size.
        var position = FormatPercent(percent);
        var shift = FormatPercent(alignment == SliderThumbAlignment.Edge ? percent : 50d);
        return orientation == SliderOrientation.Vertical
            ? $"bottom: {position}%; transform: translateY({shift}%);"
            : $"left: {position}%; transform: translateX(-{shift}%);";
    }
}
