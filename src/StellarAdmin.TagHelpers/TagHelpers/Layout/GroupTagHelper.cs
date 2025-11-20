using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sa-group")]
public class GroupTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    [HtmlAttributeName("align")]
    public GroupAlign Align { get; set; } = GroupAlign.Start;

    [HtmlAttributeName("gap")]
    public GroupGap Gap { get; set; } = GroupGap.Default;

    [HtmlAttributeName("justify")]
    public GroupJustify Justify { get; set; } = GroupJustify.Start;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "flex flex-row",
                Align switch
                {
                    GroupAlign.Center => "items-center",
                    GroupAlign.Start => "items-start",
                    GroupAlign.End => "items-end",
                    GroupAlign.Stretch => "items-stretch",
                    GroupAlign.Baseline => "items-baseline",
                    _ => "items-start",
                },
                Gap switch
                {
                    GroupGap.ExtraSmall => "gap-x-1",
                    GroupGap.Small => "gap-x-2",
                    GroupGap.Default => "gap-x-6",
                    GroupGap.Large => "gap-x-6",
                    GroupGap.ExtraLarge => "gap-x-8",
                    _ => "gap-x-4",
                },
                Justify switch
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
