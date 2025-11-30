using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-avatar")]
public class AvatarTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    [HtmlAttributeName("initials")]
    public string? Initials { get; set; }

    [HtmlAttributeName("name")]
    public string? Name { get; set; }

    [HtmlAttributeName("size")]
    public AvatarSize? Size { get; set; }

    [HtmlAttributeName("src")]
    public string? Source { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveAvatarSize = Size ?? AvatarSize.Default;
        var isRenderingText = Source == null;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "avatar");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "relative flex shrink-0 overflow-hidden rounded-full",
                isRenderingText ? "bg-muted flex size-full items-center justify-center" : null,
                isRenderingText ? GetFontSizeClass(effectiveAvatarSize) : null,
                GetSizeClass(effectiveAvatarSize),
                output.GetUserSuppliedClass()
            )
        );

        if (Source != null)
        {
            var imageTagBuilder = new TagBuilder("img");
            imageTagBuilder.Attributes.Add("data-slot", "avatar-image");
            imageTagBuilder.Attributes.Add("src", Source);
            imageTagBuilder.Attributes.Add("alt", Name);
            imageTagBuilder.Attributes.Add("class", "aspect-square size-full");

            output.Content.AppendHtml(imageTagBuilder);
        }
        else
        {
            var textToRender = GetInitials() ?? "&nbsp";
            var fallbackTagBuilder = new TagBuilder("span");
            fallbackTagBuilder.InnerHtml.AppendHtml(textToRender);

            output.Content.AppendHtml(fallbackTagBuilder);
        }

        return Task.CompletedTask;
    }

    private string? GetFontSizeClass(AvatarSize avatarSize)
    {
        return avatarSize switch
        {
            AvatarSize.ExtraSmall => "text-xs",
            AvatarSize.Small => "text-sm",
            AvatarSize.Large => "text-xl",
            AvatarSize.ExtraLarge => "text-2xl",
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

    private string GetSizeClass(AvatarSize size)
    {
        return size switch
        {
            AvatarSize.ExtraSmall => "size-6",
            AvatarSize.Small => "size-8",
            AvatarSize.Default => "size-10",
            AvatarSize.Large => "size-12",
            AvatarSize.ExtraLarge => "size-16",
            _ => "size-10",
        };
    }
}
