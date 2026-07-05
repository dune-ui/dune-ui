using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A vertical flex layout that arranges its children in a column, with configurable
///     alignment, spacing, and justification.
/// </summary>
[HtmlTargetElement("dui-stack")]
public class StackTagHelper : DuneUITagHelperBase
{
    public StackTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    /// <summary>
    ///     How the children are aligned along the cross axis (horizontally).
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="StackAlign.Stretch" />.
    /// </remarks>
    [HtmlAttributeName("align")]
    public StackAlign? Align { get; set; }

    /// <summary>
    ///     The vertical spacing between children.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="StackGap.Default" />.
    /// </remarks>
    [HtmlAttributeName("gap")]
    public StackGap? Gap { get; set; }

    /// <summary>
    ///     How the children are distributed along the main axis (vertically).
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="StackJustify.Start" />.
    /// </remarks>
    [HtmlAttributeName("justify")]
    public StackJustify? Justify { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveAlign = Align ?? StackAlign.Stretch;
        var effectiveGap = Gap ?? StackGap.Default;
        var effectiveJustify = Justify ?? StackJustify.Start;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                "flex flex-col",
                effectiveAlign switch
                {
                    StackAlign.Stretch => "items-stretch",
                    StackAlign.Center => "items-center",
                    StackAlign.Start => "items-start",
                    StackAlign.End => "items-end",
                    _ => "items-stretch",
                },
                effectiveGap switch
                {
                    StackGap.ExtraSmall => "gap-y-1",
                    StackGap.Small => "gap-y-2",
                    StackGap.Default => "gap-y-4",
                    StackGap.Large => "gap-y-6",
                    StackGap.ExtraLarge => "gap-y-8",
                    _ => "gap-y-4",
                },
                effectiveJustify switch
                {
                    StackJustify.Start => "justify-start",
                    StackJustify.End => "justify-end",
                    StackJustify.SpaceBetween => "justify-between",
                    StackJustify.SpaceAround => "justify-around",
                    StackJustify.Center => "justify-center",
                    _ => "justify-start",
                },
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
