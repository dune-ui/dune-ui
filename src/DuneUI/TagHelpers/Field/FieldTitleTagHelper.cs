using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A title for a field or field set that is styled like a label but is not associated with a control.
/// </summary>
[HtmlTargetElement("dui-field-title")]
public class FieldTitleTagHelper : DuneUITagHelperBase
{
    public FieldTitleTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "field-label");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-field-title"),
                "flex w-fit items-center leading-snug",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
