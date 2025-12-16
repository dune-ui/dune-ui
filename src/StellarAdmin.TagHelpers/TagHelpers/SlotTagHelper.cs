using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-slot", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SlotTagHelper : StellarTagHelper
{
    public SlotTagHelper(ThemeManager themeManager)
        : base(themeManager) { }

    [HtmlAttributeName("name")]
    public required string Name { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new ArgumentException("The 'name' attribute is required on a slot.");
        }

        if (ParentTagHelper is null)
        {
            throw new InvalidOperationException(
                "A slot Tag Helper can only be used inside a Stellar UI Tag Helper."
            );
        }

        var childContent = await output.GetChildContentAsync();

        if (!ParentTagHelper.TryAddNamedSlot(Name, childContent))
        {
            throw new ArgumentException(
                $"The slot named '{Name}' has already been added. You cannot add the same slot multiple times."
            );
        }

        output.SuppressOutput();
    }
}
