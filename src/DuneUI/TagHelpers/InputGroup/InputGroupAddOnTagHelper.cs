using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-input-group-addon")]
public class InputGroupAddOnTagHelper : DuneUITagHelperBase
{
    private static readonly Dictionary<
        InputGroupAddOnVariantAlignment,
        ClassElement[]
    > AlignmentClasses = new Dictionary<InputGroupAddOnVariantAlignment, ClassElement[]>
    {
        [InputGroupAddOnVariantAlignment.InlineStart] =
        [
            new ComponentName("dui-input-group-addon-align-inline-start"),
            "order-first",
        ],
        [InputGroupAddOnVariantAlignment.InlineEnd] =
        [
            new ComponentName("dui-input-group-addon-align-inline-end"),
            "order-last",
        ],
        [InputGroupAddOnVariantAlignment.BlockStart] =
        [
            new ComponentName("dui-input-group-addon-align-block-start"),
            "order-first w-full justify-start",
        ],
        [InputGroupAddOnVariantAlignment.BlockEnd] =
        [
            new ComponentName("dui-input-group-addon-align-block-end"),
            "order-last w-full justify-start",
        ],
    };

    [HtmlAttributeName("align")]
    public InputGroupAddOnVariantAlignment? Alignment { get; set; }

    public InputGroupAddOnTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveAlignment = Alignment ?? InputGroupAddOnVariantAlignment.InlineStart;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("role", "group");
        output.Attributes.SetAttribute("data-slot", "input-group-addon");
        output.Attributes.SetAttribute("data-align", effectiveAlignment.GetDataAttributeText());

        output.Attributes.SetAttribute(
            "onclick",
            """
            (function(e) {
              if ((e.target).closest('button')) {
                return;
              }
              e.currentTarget.parentElement?.querySelector('input')?.focus();
            })(event);
            """
        );

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ClassElement[]
                {
                    new ComponentName("dui-input-group-addon"),
                    "flex cursor-text items-center justify-center select-none",
                }
                    .Union(AlignmentClasses[effectiveAlignment])
                    .Append(output.GetUserSuppliedClass())
                    .ToArray()
            )
        );

        return Task.CompletedTask;
    }
}
