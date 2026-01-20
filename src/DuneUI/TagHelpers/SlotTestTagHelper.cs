namespace DuneUI.TagHelpers;

/*
[HtmlTargetElement("dui-slot-test", TagStructure = TagStructure.NormalOrSelfClosing)]
[OutputElementHint("div")]
public class SlotTestTagHelper : DuneUITagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        var childContent = await output.GetChildContentAsync();

        if (TryGetNamedSlot("slot1", out var slot1Content))
        {
            output.Content.AppendHtml(slot1Content);
        }

        output.Content.AppendHtml(childContent);
    }
}
*/
