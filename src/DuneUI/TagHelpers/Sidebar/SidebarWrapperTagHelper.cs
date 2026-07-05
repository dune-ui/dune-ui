using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     The outermost sidebar container that provides layout and shared state for the sidebar and its inset content.
///     Renders the <c>del-sidebar</c> web component that nested triggers and the backdrop toggle.
/// </summary>
[HtmlTargetElement("dui-sidebar-wrapper")]
public class SidebarWrapperTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
    : DuneUITagHelperBase(themeManager, classMerger)
{
    private const string SidebarWidth = "16rem";
    private const string SidebarWidthIcon = "3rem";
    private const string SidebarWidthMobile = "18rem";

    /// <summary>
    ///     The id of the rendered <c>del-sidebar</c> element. Nested triggers and the
    ///     backdrop read this (via <see cref="DuneUITagHelperBase.GetParentTagHelper{T}" />)
    ///     to target it with <c>commandfor</c>.
    /// </summary>
    [HtmlAttributeNotBound]
    public string? SidebarId { get; private set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        // The wrapper renders the `del-sidebar` web component, which acts as the
        // state provider. Nested triggers toggle it via the native command API.
        output.TagName = "del-sidebar";
        output.TagMode = TagMode.StartTagAndEndTag;

        // Resolve the id before child content is processed so nested triggers can
        // read it. Honour a user-supplied id; otherwise generate a stable one.
        SidebarId = output.Attributes.TryGetAttribute("id", out var idAttribute)
            ? idAttribute.Value.ToString()
            : null;
        if (SidebarId == null)
        {
            SidebarId = $"--dui-sidebar-{context.UniqueId}";
            output.Attributes.SetAttribute("id", SidebarId);
        }

        output.Attributes.SetAttribute("data-slot", "sidebar-wrapper");
        output.Attributes.SetAttribute(
            "style",
            $"--sidebar-width: {SidebarWidth}; --sidebar-width-icon: {SidebarWidthIcon}; --sidebar-width-mobile: {SidebarWidthMobile}"
        );
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                "group/sidebar-wrapper has-data-[variant=inset]:bg-sidebar flex min-h-svh w-full",
                output.GetUserSuppliedClass()
            )
        );

        output.Content.AppendHtml(await output.GetChildContentAsync());
    }
}
