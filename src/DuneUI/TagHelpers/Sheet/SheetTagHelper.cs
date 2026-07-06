using DuneUI.Icons;
using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A panel that slides in from an edge of the screen, rendered over a native
///     <c>&lt;dialog&gt;</c> element. Open and close it with the Invoker Commands API — a
///     trigger button carrying <c>commandfor</c> and <c>command="show-modal"</c> or
///     <c>command="close"</c>.
/// </summary>
[HtmlTargetElement("dui-sheet")]
public class SheetTagHelper : DuneUITagHelperBase
{
    private readonly IIconManager _iconManager;

    public SheetTagHelper(
        ThemeManager themeManager,
        ICssClassMerger classMerger,
        IIconManager iconManager
    )
        : base(themeManager, classMerger)
    {
        _iconManager = iconManager ?? throw new ArgumentNullException(nameof(iconManager));
    }

    /// <summary>
    ///     Whether to render the built-in close button in the corner of the sheet.
    /// </summary>
    /// <remarks>
    ///     Defaults to <c>true</c>.
    /// </remarks>
    [HtmlAttributeName("show-close-button")]
    public bool? ShowCloseButton { get; set; }

    /// <summary>
    ///     The edge of the screen the sheet slides in from.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="SheetSide.Right" />.
    /// </remarks>
    [HtmlAttributeName("side")]
    public SheetSide? Side { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveShowCloseButton = ShowCloseButton ?? true;
        var effectiveSide = Side ?? SheetSide.Right;

        output.TagName = "dialog";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "sheet-content");
        output.Attributes.SetAttribute("data-side", effectiveSide.GetDataAttributeText());
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                // "m-0 max-h-full max-w-full p-0 border-0", // UA style resets for dialog element
                new ThemeToken("dui-sheet-content"),
                // We define all the necessary styles below
                "transition ease-in-out data-open:animate-in data-closed:animate-out data-closed:duration-300 data-open:duration-500",
                effectiveSide switch
                {
                    SheetSide.Top =>
                        "bottom-auto inset-x-0 top-0 w-full max-w-full h-3/4 border-b data-closed:slide-out-to-top data-open:slide-in-from-top",
                    SheetSide.Right =>
                        "left-auto inset-y-0 right-0 h-full max-h-full w-3/4 border-l data-closed:slide-out-to-right data-open:slide-in-from-right",
                    SheetSide.Bottom =>
                        "top-auto inset-x-0 bottom-0 w-full max-w-full h-3/4 border-t data-closed:slide-out-to-bottom data-open:slide-in-from-bottom",
                    SheetSide.Left =>
                        "right-auto inset-y-0 left-0 h-full max-h-full w-3/4 border-r data-closed:slide-out-to-left data-open:slide-in-from-left",
                    _ => string.Empty,
                },
                "backdrop:supports-backdrop-filter:backdrop-blur-xs",
                output.GetUserSuppliedClass()
            )
        );

        // Wrap inside web component
        output.PreElement.AppendHtml("<del-dialog>");
        output.PostElement.AppendHtml("</del-dialog>");

        // Add the close button
        if (effectiveShowCloseButton)
        {
            var id = output.Attributes.TryGetAttribute("id", out var idAttribute)
                ? idAttribute.Value.ToString()
                : null;

            if (id == null)
            {
                id = $"--dui-sheet-{context.UniqueId}";
                output.Attributes.SetAttribute("id", id);
            }

            // Render the icon
            var iconOutput = new TagHelperOutput(
                "svg",
                [new TagHelperAttribute("class", "size-4")],
                (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
            );
            var iconTagHelper = new IconTagHelper(ThemeManager, ClassMerger, _iconManager)
            {
                Name = "x",
            };
            await iconTagHelper.ProcessAsync(context, iconOutput);

            // Render the button
            var buttonTagHelperOutput = new TagHelperOutput(
                string.Empty,
                [
                    new TagHelperAttribute(
                        "class",
                        ThemeManager.GetComponentClass("dui-sheet-close")
                    ),
                    new TagHelperAttribute("commandfor", id),
                    new TagHelperAttribute("command", "close"),
                ],
                (_, _) =>
                    Task.FromResult<TagHelperContent>(
                        new DefaultTagHelperContent().AppendHtml(iconOutput)
                    )
            );
            var buttonTagHelper = new ButtonTagHelper(ThemeManager, ClassMerger)
            {
                Size = ButtonSize.IconSmall,
                Variant = ButtonVariant.Ghost,
            };
            await buttonTagHelper.ProcessAsync(context, buttonTagHelperOutput);

            output.Content.AppendHtml(buttonTagHelperOutput);
        }

        // Add content wrapper
        var contentTagBuilder = new TagBuilder("div");
        contentTagBuilder.Attributes.Add("class", "flex flex-col h-full");
        contentTagBuilder.InnerHtml.AppendHtml(await output.GetChildContentAsync());
        output.Content.AppendHtml(contentTagBuilder);
    }
}
