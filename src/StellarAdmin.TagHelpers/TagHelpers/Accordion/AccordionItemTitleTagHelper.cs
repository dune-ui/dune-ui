using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Icons;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-accordion-item-title")]
public class AccordionItemTitleTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "summary";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "flex",
                "cursor-default",
                "details-disabled:pointer-events-none details-disabled:opacity-50",
                "focus-visible:border-ring focus-visible:ring-ring/50 flex flex-1 items-start justify-between gap-4 rounded-md py-4 text-left text-sm font-medium transition-all outline-none hover:underline focus-visible:ring-[3px]",
                output.GetUserSuppliedClass()
            )
        );

        // Render the content
        output.Content.AppendHtml(await output.GetChildContentAsync());

        // Render the icon
        var iconTagBuilder = new TagBuilder("div");
        iconTagBuilder.AddCssClass("text-muted-foreground");
        iconTagBuilder.InnerHtml.AppendHtml(
            """
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="block h-4 w-4 transition-all duration-300 group-open:rotate-180">
                <path d="m6 9 6 6 6-6"/>
            </svg>
            """
        );
        output.Content.AppendHtml(iconTagBuilder);
    }
}
