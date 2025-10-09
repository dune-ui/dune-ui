using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers.Avatar;

[HtmlTargetElement("sui-avatar")]
public class AvatarTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    [HtmlAttributeName("name")]
    public string? Name { get; set; }

    [HtmlAttributeName("size")]
    public AvatarSize Size { get; set; } = AvatarSize.Default;

    [HtmlAttributeName("src")]
    public string? Source { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.Add("data-slot", "avatar");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "relative flex shrink-0 overflow-hidden rounded-full",
                $"size-{GetSizeNumber()}",
                output.GetUserSpecifiedClass()
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
            var fallbackTagBuilder = new TagBuilder("div");
            fallbackTagBuilder.Attributes.Add("data-slot", "avatar-fallback");
            fallbackTagBuilder.Attributes.Add(
                "class",
                classMerger.Merge(
                    "bg-muted flex size-full items-center justify-center rounded-full",
                    GetFontSize()
                )
            );
            fallbackTagBuilder.InnerHtml.AppendHtml(textToRender);

            output.Content.AppendHtml(fallbackTagBuilder);
        }

        return Task.CompletedTask;
    }

    private string? GetFontSize()
    {
        return Size switch
        {
            AvatarSize.ExtraSmall => "text-xs",
            AvatarSize.Small => "text-sm",
            AvatarSize.Large => "text-xl",
            AvatarSize.ExtraLarge => "text-2xl",
            _ => null,
        };
    }

    private int GetSizeNumber()
    {
        return Size switch
        {
            AvatarSize.ExtraSmall => 6,
            AvatarSize.Small => 8,
            AvatarSize.Default => 10,
            AvatarSize.Large => 12,
            AvatarSize.ExtraLarge => 16,
            _ => 10,
        };
    }

    private string? GetInitials()
    {
        if (Name == null)
        {
            return null;
        }

        var splitName = Name.Split(
            ' ',
            StringSplitOptions.TrimEntries | StringSplitOptions.TrimEntries
        );

        return splitName switch
        {
            [{ Length: > 0 } first, .., { Length: > 0 } last] =>
                $"{char.ToUpper(first.AsSpan(0, 1)[0])}{char.ToUpper(last.AsSpan(0, 1)[0])}",
            _ => Name switch
            {
                [var first, var second, ..] => $"{char.ToUpper(first)}{char.ToLower(second)}",
                [var first] => $"{char.ToUpper(first)}",
                _ => null,
            },
        };
    }
}
