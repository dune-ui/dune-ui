using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

internal class FieldLabelRenderer(IStellarHtmlGenerator htmlGenerator, ICssClassMerger classMerger)
{
    public async Task Render(
        TagHelperOutput output,
        ViewContext viewContext,
        ModelExpression? modelExpression,
        string? labelText
    )
    {
        output.Attributes.SetAttribute("data-slot", "field-label");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "group/field-label peer/field-label flex w-fit gap-2 leading-snug group-data-[disabled=true]/field:opacity-50",
                "has-[>[data-slot=field]]:w-full has-[>[data-slot=field]]:flex-col has-[>[data-slot=field]]:rounded-md has-[>[data-slot=field]]:border [&>*]:data-[slot=field]:p-4",
                "has-data-[state=checked]:bg-primary/5 has-data-[state=checked]:border-primary dark:has-data-[state=checked]:bg-primary/10",
                output.GetUserSuppliedClass()
            )
        );

        var labelRenderer = new LabelRenderer(htmlGenerator, classMerger);
        await labelRenderer.Render(output, viewContext, modelExpression, labelText);
    }
}
