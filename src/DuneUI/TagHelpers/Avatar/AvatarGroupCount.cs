using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A trailing element within an avatar group that displays the count of additional,
///     unshown avatars.
/// </summary>
[HtmlTargetElement("dui-avatar-group-count")]
public class AvatarGroupCount : DuneUITagHelperBase
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
                new ThemeToken("dui-avatar-group-count"),
                "ring-background relative flex shrink-0 items-center justify-center ring-2"
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
