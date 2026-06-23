using DuneUI.Icons;
using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-sidebar-trigger")]
public class SidebarTriggerTagHelper : DuneUITagHelperBase
{
    private readonly IIconManager _iconManager;

    public SidebarTriggerTagHelper(
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
        var childContent = await output.GetChildContentAsync();

        // Default icon, unless the user supplied their own content.
        TagHelperContent iconContent;
        if (!childContent.IsEmptyOrWhiteSpace)
        {
            iconContent = childContent;
        }
        else
        {
            var iconOutput = new TagHelperOutput(
                "svg",
                [new TagHelperAttribute("class", "size-4")],
                (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
            );
            var iconTagHelper = new IconTagHelper(ThemeManager, ClassMerger, _iconManager)
            {
                Name = "panel-left",
            };
            iconTagHelper.Process(context, iconOutput);
            iconContent = new DefaultTagHelperContent().AppendHtml(iconOutput);
        }

        var attributes = new TagHelperAttributeList
        {
            new TagHelperAttribute("type", "button"),
            new TagHelperAttribute("data-slot", "sidebar-trigger"),
            new TagHelperAttribute("aria-label", "Toggle Sidebar"),
            new TagHelperAttribute("class", userClass),
        };

        // Target the parent `del-sidebar` via the native command API. When clicked,
        // the button dispatches a `command` event on that element, which toggles it.
        var sidebarId = GetParentTagHelper<SidebarWrapperTagHelper>()?.SidebarId;
        if (sidebarId != null)
        {
            attributes.Add(new TagHelperAttribute("command", "--toggle-sidebar"));
            attributes.Add(new TagHelperAttribute("commandfor", sidebarId));
        }

        var buttonOutput = new TagHelperOutput(
            string.Empty,
            attributes,
            (_, _) => Task.FromResult(new DefaultTagHelperContent().AppendHtml(iconContent))
        );

        var buttonTagHelper = new ButtonTagHelper(ThemeManager, ClassMerger)
        {
            Size = ButtonSize.IconSmall,
            Variant = ButtonVariant.Ghost,
        };
        await buttonTagHelper.ProcessAsync(context, buttonOutput);

        output.TagName = null;
        output.Content.SetHtmlContent(buttonOutput);
    }
}
