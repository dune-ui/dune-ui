using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-stack")]
public class StackTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public StackTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

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
            _classMerger.Merge(
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
