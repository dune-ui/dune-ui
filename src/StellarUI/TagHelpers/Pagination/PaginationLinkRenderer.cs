using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

internal class PaginationLinkRenderer(ICssClassMerger classMerger)
{
    public void Render(TagHelperOutput output, ButtonSize size, bool isActive)
    {
        output.TagName = "a";
        output.TagMode = TagMode.StartTagAndEndTag;

        if (isActive)
        {
            output.Attributes.SetAttribute("aria-current", "page");
        }
        output.Attributes.SetAttribute("data-slot", "pagination-link");
        output.Attributes.SetAttribute("data-active", isActive.ToString().ToLower());

        ButtonRenderingHelper.RenderAttributes(
            output,
            classMerger,
            isActive ? ButtonVariant.Outline : ButtonVariant.Ghost,
            size
        );
    }
}
