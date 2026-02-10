using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-collapsible")]
public class CollapsibleTagHelper : DuneUITagHelperBase
{
    public CollapsibleTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    [HtmlAttributeName("open")]
    public bool? IsOpen { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveIsOpen = IsOpen ?? false;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "x-data",
            AlpineJsDataSerializer.SerializeDataFunction("collapsible", [false, effectiveIsOpen])
        );
        output.Attributes.SetAttribute("x-bind", "root");

        output.Attributes.SetAttribute("data-slot", "collapsible");

        return Task.CompletedTask;
    }

    private string GetIsOpenParameterValue(bool isOpen) => isOpen ? "true" : "false";
}
