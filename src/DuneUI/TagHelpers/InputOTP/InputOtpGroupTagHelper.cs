using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A group of <c>dui-input-otp-slot</c>s within a <c>dui-input-otp</c>. Groups are separated by
///     a <c>dui-input-otp-separator</c>.
/// </summary>
[HtmlTargetElement("dui-input-otp-group")]
public class InputOtpGroupTagHelper : DuneUITagHelperBase
{
    public InputOtpGroupTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var userClass = output.GetUserSuppliedClass();

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("data-slot", "input-otp-group");
        output.Attributes.SetAttribute(
            "class",
            InputOtpRenderer.GroupClass(ClassMerger, userClass)
        );
    }
}
