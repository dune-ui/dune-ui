using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-field")]
public class FieldTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    [HtmlAttributeName("orientation")]
    public FieldOrientation Orientation { get; set; } = FieldOrientation.Vertical;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var fieldRenderer = new FieldRenderer(classMerger);
        await fieldRenderer.Render(
            output,
            Orientation,
            async () => await output.GetChildContentAsync()
        );
    }
}

internal class FieldRenderer(ICssClassMerger classMerger)
{
    private static readonly Dictionary<FieldOrientation, string[]> OrientationClasses = new()
    {
        [FieldOrientation.Vertical] = ["flex-col [&>*]:w-full [&>.sr-only]:w-auto"],
        [FieldOrientation.Horizontal] =
        [
            "flex-row items-center",
            "[&>[data-slot=field-label]]:flex-auto",
            "has-[>[data-slot=field-content]]:items-start has-[>[data-slot=field-content]]:[&>[role=checkbox],[role=radio]]:mt-px",
        ],
        [FieldOrientation.Responsive] =
        [
            "flex-col [&>*]:w-full [&>.sr-only]:w-auto @md/field-group:flex-row @md/field-group:items-center @md/field-group:[&>*]:w-auto",
            "@md/field-group:[&>[data-slot=field-label]]:flex-auto",
            "@md/field-group:has-[>[data-slot=field-content]]:items-start @md/field-group:has-[>[data-slot=field-content]]:[&>[role=checkbox],[role=radio]]:mt-px",
        ],
    };

    public async Task Render(
        TagHelperOutput output,
        FieldOrientation orientation,
        Func<Task<IHtmlContent>> renderContent
    )
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "field");
        output.Attributes.SetAttribute(
            "data-orientation",
            GetOrientationAttributeText(orientation)
        );
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                new[] { "group/field flex w-full gap-3 data-[invalid=true]:text-destructive" }
                    .Concat(OrientationClasses[orientation])
                    .Append(output.GetUserSuppliedClass())
                    .ToArray()
            )
        );

        output.Content.AppendHtml(await renderContent());
    }

    private string GetOrientationAttributeText(FieldOrientation orientation) =>
        orientation switch
        {
            FieldOrientation.Vertical => "vertical",
            FieldOrientation.Horizontal => "horizontal",
            FieldOrientation.Responsive => "responsive",
            _ => string.Empty,
        };
}
