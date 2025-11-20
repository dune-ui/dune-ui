using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-field-separator")]
public class FieldSeparatorTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        var childContent = await output.GetChildContentAsync();

        output.Attributes.SetAttribute("data-slot", "field-separator");
        output.Attributes.SetAttribute(
            "data-content",
            childContent.IsEmptyOrWhiteSpace ? "false" : "true"
        );
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "relative -my-2 h-5 text-sm group-data-[variant=outline]/field-group:-mb-2",
                output.GetUserSuppliedClass()
            )
        );

        /* Add the actual separator */
        var separatorOutput = new TagHelperOutput(
            "",
            [new TagHelperAttribute("class", "absolute inset-0 top-1/2")],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
        );
        var separatorTagHelper = new SeparatorTagHelper(classMerger)
        {
            Orientation = SeparatorOrientation.Horizontal,
        };
        await separatorTagHelper.ProcessAsync(context, separatorOutput);
        output.Content.AppendHtml(separatorOutput);

        /* Add the child content, if any */
        if (!childContent.IsEmptyOrWhiteSpace)
        {
            var contentWrapperTagBuilder = new TagBuilder("span");
            contentWrapperTagBuilder.Attributes.Add("data-slot", "field-separator-content");
            contentWrapperTagBuilder.Attributes.Add(
                "class",
                "bg-background text-muted-foreground relative mx-auto block w-fit px-2"
            );

            contentWrapperTagBuilder.InnerHtml.AppendHtml(childContent);
            output.Content.AppendHtml(contentWrapperTagBuilder);
        }
    }
}
