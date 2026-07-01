using DuneUI.Icons;
using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-dropdown-menu-radio-item")]
public class DropdownMenuRadioItemTagHelper : DuneUITagHelperBase
{
    private readonly IIconManager _iconManager;

    [HtmlAttributeName("close-on-click")]
    public bool? CloseOnClick { get; set; }

    [HtmlAttributeName("disabled")]
    public bool? Disabled { get; set; }

    [HtmlAttributeName("value")]
    public string? Value { get; set; }

    public DropdownMenuRadioItemTagHelper(
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
        var group = GetContext<DropdownMenuRadioGroupContext>(context);
        var isChecked = group?.IsSelected(Value) == true;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("role", "menuitemradio");
        output.Attributes.SetAttribute("tabindex", "-1");
        output.Attributes.SetAttribute("aria-checked", isChecked ? "true" : "false");
        output.Attributes.SetAttribute("data-slot", "dropdown-menu-radio-item");
        output.Attributes.SetAttribute("data-state", isChecked ? "checked" : "unchecked");
        if (Value != null)
        {
            output.Attributes.SetAttribute("data-value", Value);
        }

        if (group != null)
        {
            output.Attributes.SetAttribute("data-radio-group", group.GroupName);
        }

        // Only emit when the author overrides the default (radio items stay open on click).
        if (CloseOnClick.HasValue)
        {
            output.Attributes.SetAttribute(
                "data-close-on-click",
                CloseOnClick.Value ? "true" : "false"
            );
        }

        if (Disabled == true)
        {
            output.Attributes.SetAttribute("data-disabled", "");
            output.Attributes.SetAttribute("aria-disabled", "true");
        }

        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ThemeToken("dui-dropdown-menu-radio-item"),
                "relative flex cursor-default items-center outline-hidden select-none data-disabled:pointer-events-none data-disabled:opacity-50 [&_svg]:pointer-events-none [&_svg]:shrink-0",
                output.GetUserSuppliedClass()
            )
        );

        var indicator = new TagBuilder("span");
        indicator.Attributes["data-slot"] = "dropdown-menu-radio-item-indicator";
        indicator.Attributes["class"] = ClassMerger.Merge(
            new ThemeToken("dui-dropdown-menu-item-indicator"),
            "pointer-events-none",
            isChecked ? string.Empty : "hidden"
        );
        indicator.InnerHtml.AppendHtml(
            DropdownMenuInternals.RenderIcon(
                context,
                ThemeManager,
                ClassMerger,
                _iconManager,
                "check",
                "size-4"
            )
        );

        var childContent = await output.GetChildContentAsync();
        output.Content.SetHtmlContent(indicator);
        output.Content.AppendHtml(childContent);
    }
}
