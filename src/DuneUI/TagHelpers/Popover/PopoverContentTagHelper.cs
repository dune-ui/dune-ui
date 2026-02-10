using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-popover-content")]
public class PopoverContentTagHelper : DuneUITagHelperBase
{
    [HtmlAttributeName("teleport")]
    public string? Teleport { get; set; }

    public PopoverContentTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
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
        containerTagBuilder.Attributes.Add("x-bind", "content");
        containerTagBuilder.Attributes.Add(
            "class",
            BuildClassString(
                null,
                [
                    "z-50 w-72 rounded-md border bg-popover p-4 text-popover-foreground shadow-md outline-none",
                    output.GetUserSuppliedClass(),
                ]
            )
        );
        containerTagBuilder.InnerHtml.AppendHtml(await output.GetChildContentAsync());

        output.Content.AppendHtml(containerTagBuilder);
    }
}
