using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-option")]
public class OptionTagHelper : StellarTagHelper
{
    [HtmlAttributeName("value")]
    public string? Value { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "option";
        output.TagMode = TagMode.StartTagAndEndTag;

        if (Value != null)
        {
            output.CopyHtmlAttribute(nameof(Value), context);
        }

        if (GetParentTagHelper<SelectTagHelper>() is { } parentTagHelper)
        {
            var selected = false;
            if (
                Value != null
                && parentTagHelper is { SelectedValues: { Count: > 0 } selectedValues }
            )
            {
                selected = selectedValues.Contains(Value);
            }
            else if (
                parentTagHelper is
                { SelectedRawAndEncodedValues: { Count: > 0 } selectedRawAndEncodedValues }
            )
            {
                // If we do not have a Value property set, we must match on the content of the option.
                // For that, we must compare against both the raw values, as well as their encoded
                // representations, since they will be encoded inside the option body
                var childContent = output.IsContentModified
                    ? output.Content
                    : await output.GetChildContentAsync();

                selected = selectedRawAndEncodedValues.Contains(childContent.GetContent());
            }

            if (selected)
            {
                output.Attributes.Add("selected", "selected");
            }
        }

        output.Content.SetHtmlContent(await output.GetChildContentAsync());
    }
}
