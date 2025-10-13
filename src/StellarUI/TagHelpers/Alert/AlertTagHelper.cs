using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-alert")]
public class AlertTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    private static readonly Dictionary<AlertVariant, string> AlertVariantClasses = new()
    {
        [AlertVariant.Default] = "bg-card text-card-foreground",
        [AlertVariant.Destructive] =
            "text-destructive bg-card [&>svg]:text-current *:data-[slot=alert-description]:text-destructive/90",
    };

    [HtmlAttributeName("variant")]
    public AlertVariant Variant { get; set; } = AlertVariant.Default;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "alert");
        output.Attributes.SetAttribute("role", "alert");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "relative w-full rounded-lg border px-4 py-3 text-sm grid has-[>svg]:grid-cols-[calc(var(--spacing)*4)_1fr] grid-cols-[0_1fr] has-[>svg]:gap-x-3 gap-y-0.5 items-start [&>svg]:size-4 [&>svg]:translate-y-0.5 [&>svg]:text-current",
                AlertVariantClasses[Variant],
                output.GetUserSuppliedClass()
            )
        );

        output.Content.SetHtmlContent(await output.GetChildContentAsync());
    }
}
