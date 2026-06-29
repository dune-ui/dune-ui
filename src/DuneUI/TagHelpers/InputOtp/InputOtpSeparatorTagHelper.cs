using DuneUI.Icons;
using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A separator placed between <c>dui-input-otp-group</c>s. Renders a Lucide <c>minus</c> icon
///     by default; supply child content to override it.
/// </summary>
[HtmlTargetElement("dui-input-otp-separator")]
public class InputOtpSeparatorTagHelper : DuneUITagHelperBase
{
    private readonly IIconManager _iconManager;

    public InputOtpSeparatorTagHelper(
        ThemeManager themeManager,
        ICssClassMerger classMerger,
        IIconManager iconManager
    )
        : base(themeManager, classMerger)
    {
        _iconManager = iconManager ?? throw new ArgumentNullException(nameof(iconManager));
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var userClass = output.GetUserSuppliedClass();

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("data-slot", "input-otp-separator");
        output.Attributes.SetAttribute("role", "separator");
        output.Attributes.SetAttribute(
            "class",
            InputOtpRenderer.SeparatorClass(ClassMerger, userClass)
        );

        var childContent = await output.GetChildContentAsync();
        if (!childContent.IsEmptyOrWhiteSpace)
        {
            output.Content.AppendHtml(childContent);
        }
        else
        {
            await InputOtpRenderer.RenderDefaultSeparatorContentAsync(
                output.Content,
                context,
                ThemeManager,
                ClassMerger,
                _iconManager
            );
        }
    }
}
