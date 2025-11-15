using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-field-description")]
public class FieldDescriptionTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    private const string ForAttributeName = "asp-for";

    /// <summary>
    /// An expression to be evaluated against the current model.
    /// </summary>
    [HtmlAttributeName(ForAttributeName)]
    public ModelExpression? For { get; set; }

    /// <summary>
    /// Gets the <see cref="ViewContext"/> of the executing view.
    /// </summary>
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "field-description");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "text-muted-foreground text-sm leading-normal font-normal group-has-[[data-orientation=horizontal]]/field:text-balance",
                "last:mt-0 nth-last-2:-mt-1 [[data-variant=legend]+&]:-mt-1.5",
                "[&>a:hover]:text-primary [&>a]:underline [&>a]:underline-offset-4",
                output.GetUserSuppliedClass()
            )
        );

        var childContent = await output.GetChildContentAsync();
        if (childContent.IsEmptyOrWhiteSpace)
        {
            if (For?.Metadata.Description is { Length: > 0 } description)
            {
                output.Content.SetContent(description);
            }
            else
            {
                // If we are trying to resolve the description via the metadata (i.e. when using asp-for)
                // but there is not description, we suppress output to prevent this control from rendering.
                // If we don't do this, it generates an empty div with a gap around it which takes up
                // unnecessary space.
                output.SuppressOutput();
            }
        }
        else
        {
            output.Content.AppendHtml(childContent);
        }
    }
}
