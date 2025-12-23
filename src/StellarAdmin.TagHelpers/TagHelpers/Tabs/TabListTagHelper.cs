using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-tab-list")]
public class TabListTagHelper : StellarTagHelper
{
    [HtmlAttributeName("orientation")]
    public TabListOrientation? Orientation { get; set; }

    [HtmlAttributeName("variant")]
    public TabListVariant? Variant { get; set; }

    public TabListTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveOrientation = Orientation ?? TabListOrientation.Horizontal;
        var effectiveVariant = Variant ?? TabListVariant.Default;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "tabs");
        output.Attributes.SetAttribute(
            "data-orientation",
            effectiveOrientation.GetDataAttributeText()
        );

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-tabs"),
                "group/tabs flex data-[orientation=horizontal]:flex-col",
                output.GetUserSuppliedClass()
            )
        );

        var tabListTagBuilder = new TagBuilder("div");
        tabListTagBuilder.Attributes.Add("data-slot", "tabs-list");
        tabListTagBuilder.Attributes.Add("data-variant", effectiveVariant.GetDataAttributeText());
        tabListTagBuilder.Attributes.Add(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-tabs-list"),
                "group/tabs-list text-muted-foreground inline-flex w-fit items-center justify-center group-data-[orientation=vertical]/tabs:h-fit group-data-[orientation=vertical]/tabs:flex-col",
                effectiveVariant == TabListVariant.Default
                    ? new ComponentName("dui-tabs-list-variant-default")
                    : new ComponentName("dui-tabs-list-variant-line"),
                effectiveVariant == TabListVariant.Default ? "bg-muted" : "gap-1 bg-transparent",
                output.GetUserSuppliedClass()
            )
        );
        tabListTagBuilder.InnerHtml.AppendHtml(await output.GetChildContentAsync());

        output.Content.AppendHtml(tabListTagBuilder);
    }
}
