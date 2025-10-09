using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers.Avatar;

[HtmlTargetElement("sui-avatar-group")]
public class AvatarGroupTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.Add(
            "class",
            classMerger.Merge(
                "*:data-[slot=avatar]:ring-background flex -space-x-2 *:data-[slot=avatar]:ring-2",
                output.GetUserSpecifiedClass()
            )
        );

        await base.ProcessAsync(context, output);
    }
}
