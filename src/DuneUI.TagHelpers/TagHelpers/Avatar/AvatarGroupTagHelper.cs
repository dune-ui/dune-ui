using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-avatar-group")]
public class AvatarGroupTagHelper : DuneUITagHelperBase
{
    public AvatarGroupTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.Add("data-slot", "avatar-group");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ComponentName("dui-avatar-group"),
                "*:data-[slot=avatar]:ring-background group/avatar-group flex -space-x-2 *:data-[slot=avatar]:ring-2",
                output.GetUserSuppliedClass()
            )
        );

        await base.ProcessAsync(context, output);
    }
}
