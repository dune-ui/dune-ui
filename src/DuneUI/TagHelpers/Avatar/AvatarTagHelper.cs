using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-avatar")]
public class AvatarTagHelper : DuneUITagHelperBase
{
    public AvatarTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    [HtmlAttributeName("initials")]
    public string? Initials { get; set; }

    [HtmlAttributeName("name")]
    public string? Name { get; set; }

    [HtmlAttributeName("size")]
    public AvatarSize? Size { get; set; }

    [HtmlAttributeName("src")]
    public string? Source { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveAvatarSize = Size ?? AvatarSize.Default;

        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "avatar");
        output.Attributes.SetAttribute("data-size", effectiveAvatarSize.GetDataAttributeText());
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ComponentName("dui-avatar"),
                "after:border-border group/avatar relative flex shrink-0 select-none after:absolute after:inset-0 after:border after:mix-blend-darken dark:after:mix-blend-lighten",
                output.GetUserSuppliedClass()
            )
        );

        if (Source != null)
        {
            var imageTagBuilder = new TagBuilder("img");
            imageTagBuilder.Attributes.Add("data-slot", "avatar-image");
            imageTagBuilder.Attributes.Add("src", Source);
            imageTagBuilder.Attributes.Add("alt", Name);
            imageTagBuilder.Attributes.Add(
                "class",
                BuildClassString(
                    new ComponentName("dui-avatar-image"),
                    "aspect-square size-full object-cover"
                )
            );
            output.Content.AppendHtml(imageTagBuilder);
        }
        else
        {
            var textToRender = GetInitials() ?? "&nbsp";
            var fallbackTagBuilder = new TagBuilder("span");
            fallbackTagBuilder.Attributes.Add("data-slot", "avatar-fallback");
            fallbackTagBuilder.Attributes.Add(
                "class",
                BuildClassString(
                    new ComponentName("dui-avatar-fallback"),
                    "flex size-full items-center justify-center text-sm group-data-[size=sm]/avatar:text-xs"
                )
            );
            fallbackTagBuilder.InnerHtml.AppendHtml(textToRender);
            output.Content.AppendHtml(fallbackTagBuilder);
        }

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }

    private string? GetFontSizeClass(AvatarSize avatarSize)
    {
        return avatarSize switch
        {
            AvatarSize.Small => "text-sm",
            AvatarSize.Large => "text-xl",
            _ => null,
        };
    }

    private string? GetInitials()
    {
        return (Initials, Name) switch
        {
            ({ } initials, _) => initials,
            (_, { } name) => DetermineInitialsFromName(name),
            _ => null,
        };

        string? DetermineInitialsFromName(string name)
        {
            var splitName = name.Split(
                ' ',
                StringSplitOptions.TrimEntries | StringSplitOptions.TrimEntries
            );

            return splitName switch
            {
                [{ Length: > 0 } first, .., { Length: > 0 } last] =>
                    $"{char.ToUpper(first.AsSpan(0, 1)[0])}{char.ToUpper(last.AsSpan(0, 1)[0])}",
                _ => name switch
                {
                    [var first, var second, ..] => $"{char.ToUpper(first)}{char.ToLower(second)}",
                    [var first] => $"{char.ToUpper(first)}",
                    _ => null,
                },
            };
        }
    }
}
