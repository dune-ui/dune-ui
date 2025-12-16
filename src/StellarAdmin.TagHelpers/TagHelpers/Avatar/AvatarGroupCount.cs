using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-avatar-group-count")]
public class AvatarGroupCount : StellarTagHelper
{
    public AvatarGroupCount(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "avatar-group-count");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ComponentName("dui-avatar-group-count"),
                "ring-background relative flex shrink-0 items-center justify-center ring-2"
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
