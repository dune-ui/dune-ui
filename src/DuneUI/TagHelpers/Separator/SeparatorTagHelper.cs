using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A thin dividing line between sections of content, rendered as a <c>&lt;div&gt;</c>
///     with <c>role="separator"</c>.
/// </summary>
[HtmlTargetElement("dui-separator")]
public class SeparatorTagHelper : DuneUITagHelperBase
{
    private static readonly Dictionary<SeparatorOrientation, ThemeToken> OrientationClasses = new()
    {
        [SeparatorOrientation.Horizontal] = new ThemeToken("dui-separator-horizontal"),
        [SeparatorOrientation.Vertical] = new ThemeToken("dui-separator-vertical"),
    };

    public SeparatorTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    /// <summary>
    ///     The orientation of the separator.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="SeparatorOrientation.Horizontal" />.
    /// </remarks>
    [HtmlAttributeName("orientation")]
    public SeparatorOrientation? Orientation { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveOrientation = Orientation ?? SeparatorOrientation.Horizontal;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("role", "separator");
        output.Attributes.SetAttribute(
            "aria-orientation",
            effectiveOrientation.GetDataAttributeText()
        );
        output.Attributes.Add("data-slot", "separator");
        output.Attributes.SetAttribute(
            "data-orientation",
            effectiveOrientation.GetDataAttributeText()
        );

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-separator"),
                OrientationClasses[effectiveOrientation],
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
