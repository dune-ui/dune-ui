using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-stack")]
public class StackTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    [HtmlAttributeName("align")]
    public StackAlign Align { get; set; } = StackAlign.Stretch;

    [HtmlAttributeName("gap")]
    public StackGap Gap { get; set; } = StackGap.Default;

    [HtmlAttributeName("justify")]
    public StackJustify Justify { get; set; } = StackJustify.Start;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "flex flex-col",
                Align switch
                {
                    StackAlign.Stretch => "items-stretch",
                    StackAlign.Center => "items-center",
                    StackAlign.Start => "items-start",
                    StackAlign.End => "items-end",
                    _ => "items-stretch",
                },
                Gap switch
                {
                    StackGap.ExtraSmall => "gap-y-1",
                    StackGap.Small => "gap-y-2",
                    StackGap.Default => "gap-y-4",
                    StackGap.Large => "gap-y-6",
                    StackGap.ExtraLarge => "gap-y-8",
                    _ => "gap-y-4",
                },
                Justify switch
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
