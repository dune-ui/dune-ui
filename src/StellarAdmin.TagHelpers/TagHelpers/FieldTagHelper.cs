namespace StellarUI.TagHelpers;

/*
[HtmlTargetElement("sui-field")]
public class FieldTagHelper(ICssClassMerger cssClassMerger) : StellarTagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        string[] classes = ["min-w-0"];
        var explicitClassNames =
            output.Attributes.TryGetAttribute("class", out var classAttr)
            && classAttr.Value.ToString() is { } classValue
                ? classValue
                : null;

        var x = classes.Concat([explicitClassNames]).ToArray();
        var mergedClassNames = cssClassMerger.Merge(x);
        output.Attributes.SetAttribute("class", mergedClassNames);

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
*/
