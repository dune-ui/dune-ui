using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-input-group-button")]
public class InputGroupButtonTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    private static readonly Dictionary<InputGroupButtonSize, string> SizeClasses = new Dictionary<
        InputGroupButtonSize,
        string
    >
    {
        [InputGroupButtonSize.ExtraSmall] =
            "h-6 gap-1 px-2 rounded-[calc(var(--radius)-5px)] [&>svg:not([class*='size-'])]:size-3.5 has-[>svg]:px-2",
        [InputGroupButtonSize.Small] = "h-8 px-2.5 gap-1.5 rounded-md has-[>svg]:px-2.5",
        [InputGroupButtonSize.IconExtraSmall] =
            "size-6 rounded-[calc(var(--radius)-5px)] p-0 has-[>svg]:p-0",
        [InputGroupButtonSize.IconSmall] = "size-8 p-0 has-[>svg]:p-0",
    };

    [HtmlAttributeName("variant")]
    public ButtonVariant? Variant { get; set; }

    [HtmlAttributeName("size")]
    public InputGroupButtonSize? Size { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveSize = Size ?? InputGroupButtonSize.ExtraSmall;

        output.TagName = "button";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-size", effectiveSize.GetDataAttributeText());
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "text-sm shadow-none flex gap-2 items-center",
                SizeClasses[effectiveSize],
                output.GetUserSuppliedClass()
            )
        );

        ButtonRenderingHelper.RenderAttributes(
            output,
            classMerger,
            Variant ?? ButtonVariant.Ghost,
            ButtonSize.Default
        );

        return base.ProcessAsync(context, output);
    }
}
