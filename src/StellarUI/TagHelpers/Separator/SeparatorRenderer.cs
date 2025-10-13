using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

internal class SeparatorRenderer(ICssClassMerger classMerger)
{
    public async Task Render(
        TagHelperOutput output,
        SeparatorOrientation orientation,
        bool isDecorative
    )
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "bg-border shrink-0 data-[orientation=horizontal]:h-px data-[orientation=horizontal]:w-full data-[orientation=vertical]:h-full data-[orientation=vertical]:w-px",
                output.GetUserSuppliedClass()
            )
        );
        output.Attributes.SetAttribute("data-role", isDecorative ? "none" : "separator");
        output.Attributes.SetAttribute("data-orientation", orientation.GetValue());
    }
}
