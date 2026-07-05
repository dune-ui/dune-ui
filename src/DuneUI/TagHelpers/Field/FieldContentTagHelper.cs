using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A container that holds a field's label and description, keeping them stacked together
///     when the field is laid out horizontally alongside its control.
/// </summary>
[HtmlTargetElement("dui-field-content")]
public class FieldContentTagHelper : DuneUITagHelperBase
{
    public FieldContentTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "field-content");
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-field-content"),
                "group/field-content flex flex-1 flex-col leading-snug",
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
