using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-empty-media")]
public class EmptyMediaTagHelper : DuneUITagHelperBase
{
    private static readonly Dictionary<EmptyMediaVariant, ComponentName> VariantClasses =
        new Dictionary<EmptyMediaVariant, ComponentName>
        {
            [EmptyMediaVariant.Default] = new ComponentName("dui-empty-media-default"),
            [EmptyMediaVariant.Icon] = new ComponentName("dui-empty-media-icon"),
        };

    [HtmlAttributeName("variant")]
    public EmptyMediaVariant? Variant { get; set; }

    public EmptyMediaTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveVariant = Variant ?? EmptyMediaVariant.Default;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "empty-icon");
        output.Attributes.SetAttribute("data-variant", effectiveVariant.GetDataAttributeText());
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-empty-media"),
                "flex shrink-0 items-center justify-center [&_svg]:pointer-events-none [&_svg]:shrink-0",
                VariantClasses[effectiveVariant],
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
