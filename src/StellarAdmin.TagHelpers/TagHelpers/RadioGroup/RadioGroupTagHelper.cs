namespace StellarUI.TagHelpers;

/*
[HtmlTargetElement("sa-radio-group")]
public class RadioGroupTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("role", "radiogroup");
        output.Attributes.SetAttribute("data-slot", "radio-group");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge("grid gap-3", output.GetUserSuppliedClass())
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
*/
