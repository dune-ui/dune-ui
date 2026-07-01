using DuneUI.Icons;
using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-dropdown-menu-sub-trigger")]
public class DropdownMenuSubTriggerTagHelper : DuneUITagHelperBase
{
    private readonly IIconManager _iconManager;

    [HtmlAttributeName("inset")]
    public bool? Inset { get; set; }

    public DropdownMenuSubTriggerTagHelper(
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
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("role", "menuitem");
        output.Attributes.SetAttribute("tabindex", "-1");
        output.Attributes.SetAttribute("aria-haspopup", "menu");
        output.Attributes.SetAttribute("data-slot", "dropdown-menu-sub-trigger");

        var subId = GetContext<DropdownMenuContext>(context)?.MenuId;
        if (subId != null)
        {
            output.Attributes.SetAttribute("popovertarget", subId);
            output.Attributes.SetAttribute("interestfor", subId);

            // The sub-trigger is a <div>, so `popovertarget` doesn't establish the implicit
            // CSS anchor the way a <button> trigger does. Name it explicitly (reusing SubId,
            // a valid dashed-ident) so the sub-content can position against it.
            output.AppendStyle($"anchor-name: {subId}");
        }

        if (Inset == true)
        {
            output.Attributes.SetAttribute("data-inset", "true");
        }

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-dropdown-menu-sub-trigger"),
                "flex cursor-default items-center outline-hidden select-none [&_svg]:pointer-events-none [&_svg]:shrink-0",
                output.GetUserSuppliedClass()
            )
        );

        var childContent = await output.GetChildContentAsync();
        output.Content.SetHtmlContent(childContent);
        output.Content.AppendHtml(
            DropdownMenuInternals.RenderIcon(
                context,
                ThemeManager,
                ClassMerger,
                _iconManager,
                "chevron-right",
                "ml-auto size-4"
            )
        );
    }
}
