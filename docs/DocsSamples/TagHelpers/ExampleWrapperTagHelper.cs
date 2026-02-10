using DuneUI;
using DuneUI.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DocsSamples.TagHelpers;

[HtmlTargetElement("docs-example-wrapper")]
public class ExampleWrapperTagHelper(ICssClassMerger classMerger) : TagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        var innerTagBuilder = new TagBuilder("div");
        innerTagBuilder.Attributes.Add("data-slot", "example-wrapper");
        innerTagBuilder.Attributes.Add(
            "class",
            classMerger.Merge(
                "mx-auto grid min-h-screen w-full max-w-5xl min-w-0 content-center items-start gap-8 p-4 pt-2 sm:gap-12 sm:p-6 md:grid-cols-2 md:gap-8 lg:p-12 2xl:max-w-6xl",
                output.GetUserSuppliedClass()
            )
        );
        innerTagBuilder.InnerHtml.AppendHtml(await output.GetChildContentAsync());

        output.Attributes.SetAttribute("class", "bg-background w-full");
        output.Content.AppendHtml(innerTagBuilder);
    }
}
