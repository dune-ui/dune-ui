using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-accordion-item-title")]
public class AccordionItemTitleTagHelper : DuneUITagHelperBase
{
    public AccordionItemTitleTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "summary";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ComponentName("dui-accordion-trigger"),
                "group/accordion-trigger relative flex flex-1 items-start justify-between border border-transparent transition-all outline-none disabled:pointer-events-none disabled:opacity-50",
                "cursor-default",
                "details-disabled:pointer-events-none details-disabled:opacity-50",
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
