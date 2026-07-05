using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A horizontal flex layout that arranges its children in a row, with configurable
///     alignment, spacing, and justification.
/// </summary>
[HtmlTargetElement("dui-group")]
public class GroupTagHelper : DuneUITagHelperBase
{
    public GroupTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    /// <summary>
    ///     How the children are aligned along the cross axis (vertically).
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="GroupAlign.Start" />.
    /// </remarks>
    [HtmlAttributeName("align")]
    public GroupAlign? Align { get; set; }

    /// <summary>
    ///     The horizontal spacing between children.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="GroupGap.Default" />.
    /// </remarks>
    [HtmlAttributeName("gap")]
    public GroupGap? Gap { get; set; }

    /// <summary>
    ///     How the children are distributed along the main axis (horizontally).
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="GroupJustify.Start" />.
    /// </remarks>
    [HtmlAttributeName("justify")]
    public GroupJustify? Justify { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveAlign = Align ?? GroupAlign.Start;
        var effectiveGap = Gap ?? GroupGap.Default;
        var effectiveJustify = Justify ?? GroupJustify.Start;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                "flex flex-row",
                effectiveAlign switch
                {
                    GroupAlign.Center => "items-center",
                    GroupAlign.Start => "items-start",
                    GroupAlign.End => "items-end",
                    GroupAlign.Stretch => "items-stretch",
                    GroupAlign.Baseline => "items-baseline",
                    _ => "items-start",
                },
                effectiveGap switch
                {
                    GroupGap.ExtraSmall => "gap-x-1",
                    GroupGap.Small => "gap-x-2",
                    GroupGap.Default => "gap-x-6",
                    GroupGap.Large => "gap-x-6",
                    GroupGap.ExtraLarge => "gap-x-8",
                    _ => "gap-x-4",
                },
                effectiveJustify switch
                {
                    GroupJustify.Start => "justify-start",
                    GroupJustify.End => "justify-end",
                    GroupJustify.SpaceBetween => "justify-between",
                    GroupJustify.SpaceAround => "justify-around",
                    GroupJustify.Center => "justify-center",
                    _ => "justify-start",
                },
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
