using System.Globalization;
using DuneUI.Icons;
using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     Shared rendering for the Input OTP parts so the host's auto-generated markup is identical
///     to the markup produced when the author composes <c>dui-input-otp-group</c> /
///     <c>dui-input-otp-slot</c> / <c>dui-input-otp-separator</c> by hand.
/// </summary>
internal static class InputOtpRenderer
{
    internal static string GroupClass(ICssClassMerger classMerger, string? userClass) =>
        classMerger.Merge(new ThemeToken("dui-input-otp-group"), "flex items-center", userClass)
        ?? string.Empty;

    internal static string SlotClass(ICssClassMerger classMerger, string? userClass) =>
        classMerger.Merge(
            new ThemeToken("dui-input-otp-slot"),
            "relative flex items-center justify-center data-[active=true]:z-10",
            userClass
        ) ?? string.Empty;

    internal static string SeparatorClass(ICssClassMerger classMerger, string? userClass) =>
        classMerger.Merge(new ThemeToken("dui-input-otp-separator"), "flex items-center", userClass)
        ?? string.Empty;

    /// <summary>
    ///     Builds a single presentational slot cell. The character (if any) seeds the first paint;
    ///     the web component re-distributes the live value once hydrated.
    /// </summary>
    internal static TagBuilder BuildSlot(
        ICssClassMerger classMerger,
        int index,
        string? character,
        bool hasError
    )
    {
        var slot = new TagBuilder("div");
        slot.Attributes.Add("data-slot", "input-otp-slot");
        slot.Attributes.Add("data-index", index.ToString(CultureInfo.InvariantCulture));
        slot.Attributes.Add("data-active", "false");
        if (hasError)
        {
            slot.Attributes.Add("aria-invalid", "true");
        }
        slot.Attributes.Add("class", SlotClass(classMerger, null));
        if (!string.IsNullOrEmpty(character))
        {
            slot.InnerHtml.Append(character);
        }
        return slot;
    }

    /// <summary>
    ///     Renders the default separator glyph (a Lucide <c>minus</c> icon) into the given content.
    /// </summary>
    internal static async Task RenderDefaultSeparatorContentAsync(
        TagHelperContent target,
        TagHelperContext context,
        ThemeManager themeManager,
        ICssClassMerger classMerger,
        IIconManager iconManager
    )
    {
        var iconOutput = new TagHelperOutput(
            "svg",
            [new TagHelperAttribute("class", "size-4")],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
        );
        var iconTagHelper = new IconTagHelper(themeManager, classMerger, iconManager)
        {
            Name = "minus",
        };
        await iconTagHelper.ProcessAsync(context, iconOutput);
        target.AppendHtml(iconOutput);
    }
}
