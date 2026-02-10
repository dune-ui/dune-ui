using DuneUI;
using DuneUI.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DocsSamples.TagHelpers;

[HtmlTargetElement("docs-example")]
public class ExampleTagHelper(ICssClassMerger classMerger) : TagHelper
{
    [HtmlAttributeName("container-class")]
    public string? ContainerClass { get; set; }

    [HtmlAttributeName("title")]
    public string? Title { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        var titleTagBuilder = new TagBuilder("div");
        titleTagBuilder.Attributes.Add(
            "class",
            "text-muted-foreground px-1.5 py-2 text-xs font-medium"
        );
        titleTagBuilder.InnerHtml.Append(Title ?? string.Empty);
        output.Content.AppendHtml(titleTagBuilder);

        var contentTagBuilder = new TagBuilder("div");
        contentTagBuilder.Attributes.Add("data-slot", "example-content");
        contentTagBuilder.Attributes.Add(
            "class",
            classMerger.Merge(
                "bg-background text-foreground flex min-w-0 flex-1 flex-col items-start gap-6 border border-dashed p-4 sm:p-6 *:[div:not([class*='w-'])]:w-full",
                output.GetUserSuppliedClass()
            )
        );
        contentTagBuilder.InnerHtml.AppendHtml(await output.GetChildContentAsync());
        output.Content.AppendHtml(contentTagBuilder);

        output.Attributes.SetAttribute("data-slot", "example");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "mx-auto flex w-full max-w-lg min-w-0 flex-col gap-1 self-stretch lg:max-w-none",
                ContainerClass
            )
        );
    }
}
