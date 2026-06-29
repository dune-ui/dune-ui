using System.Globalization;
using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A single presentational slot cell within a <c>dui-input-otp</c>. Displays one character of
///     the code and the active caret. The slot self-assigns its index from the parent's running
///     counter unless an explicit <c>index</c> is set.
/// </summary>
[HtmlTargetElement("dui-input-otp-slot")]
public class InputOtpSlotTagHelper : DuneUITagHelperBase
{
    public InputOtpSlotTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    /// <summary>
    ///     The zero-based position of this slot. When omitted, the slot takes the next position in
    ///     document order.
    /// </summary>
    [HtmlAttributeName("index")]
    public int? Index { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var inputOtpContext = GetContext<InputOtpContext>(context);

        int index;
        if (Index.HasValue)
        {
            index = Index.Value;
        }
        else if (inputOtpContext != null)
        {
            index = inputOtpContext.NextIndex;
            inputOtpContext.NextIndex++;
        }
        else
        {
            index = 0;
        }

        var hasError = inputOtpContext?.HasError ?? false;
        var userClass = output.GetUserSuppliedClass();

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("data-slot", "input-otp-slot");
        output.Attributes.SetAttribute("data-index", index.ToString(CultureInfo.InvariantCulture));
        output.Attributes.SetAttribute("data-active", "false");
        if (hasError)
        {
            output.Attributes.SetAttribute("aria-invalid", "true");
        }
        output.Attributes.SetAttribute("class", InputOtpRenderer.SlotClass(ClassMerger, userClass));

        var character = inputOtpContext?.CharAt(index);
        if (!string.IsNullOrEmpty(character))
        {
            output.Content.SetContent(character);
        }
        else
        {
            output.Content.SetContent(string.Empty);
        }
    }
}
