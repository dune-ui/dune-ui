using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-input-group-addon")]
public class InputGroupAddOnTagHelper : StellarTagHelper
{
    private readonly ICssClassMerger _classMerger;

    public InputGroupAddOnTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager)
    {
        _classMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    private static readonly Dictionary<InputGroupAddOnVariantAlignment, string> AlignmentClasses =
        new Dictionary<InputGroupAddOnVariantAlignment, string>
        {
            [InputGroupAddOnVariantAlignment.InlineStart] =
                "order-first pl-3 has-[>button]:ml-[-0.45rem] has-[>kbd]:ml-[-0.35rem]",
            [InputGroupAddOnVariantAlignment.InlineEnd] =
                "order-last pr-3 has-[>button]:mr-[-0.45rem] has-[>kbd]:mr-[-0.35rem]",
            [InputGroupAddOnVariantAlignment.BlockStart] =
                "order-first w-full justify-start px-3 pt-3 [.border-b]:pb-3 group-has-[>input]/input-group:pt-2.5",
            [InputGroupAddOnVariantAlignment.BlockEnd] =
                "order-last w-full justify-start px-3 pb-3 [.border-t]:pt-3 group-has-[>input]/input-group:pb-2.5",
        };

    [HtmlAttributeName("align")]
    public InputGroupAddOnVariantAlignment? Alignment { get; set; }

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
            _classMerger.Merge(
                "text-muted-foreground flex h-auto cursor-text items-center justify-center gap-2 py-1.5 text-sm font-medium select-none [&>svg:not([class*='size-'])]:size-4 [&>kbd]:rounded-[calc(var(--radius)-5px)] group-data-[disabled=true]/input-group:opacity-50",
                AlignmentClasses[effectiveAlignment],
                output.GetUserSuppliedClass()
            )
        );

        return Task.CompletedTask;
    }
}
