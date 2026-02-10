using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-tooltip-content")]
public class TooltipContentTagHelper : DuneUITagHelperBase
{
    [HtmlAttributeName("teleport")]
    public string? Teleport { get; set; }

    public TooltipContentTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (!string.IsNullOrWhiteSpace(Teleport))
        {
            output.TagName = "template";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("x-teleport", Teleport);
        }
        else
        {
            output.TagName = string.Empty;
        }

        var containerTagBuilder = new TagBuilder("div");
        containerTagBuilder.Attributes.Add("x-cloak", null);

        var contentTagBuilder = new TagBuilder("div");
        contentTagBuilder.Attributes.Add("x-ref", "content");
        contentTagBuilder.Attributes.Add("x-bind", "content");
        contentTagBuilder.Attributes.Add("data-slot", "tooltip-content");
        contentTagBuilder.Attributes.Add(
            "class",
            BuildClassString(
                null,
                [
                    "z-50 bg-foreground text-background overflow-hidden rounded-md px-3 py-1.5 text-xs text-balance",
                    "animate-in fade-in-0 zoom-in-95 data-[state=closed]:animate-out data-[state=closed]:fade-out-0 data-[state=closed]:zoom-out-95",
                    output.GetUserSuppliedClass(),
                ]
            )
        );
        contentTagBuilder.InnerHtml.AppendHtml(await output.GetChildContentAsync());
        containerTagBuilder.InnerHtml.AppendHtml(contentTagBuilder);

        output.Content.AppendHtml(containerTagBuilder);
    }
}
