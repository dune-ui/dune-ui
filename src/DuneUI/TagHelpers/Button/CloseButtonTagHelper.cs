using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

[HtmlTargetElement("dui-button", Attributes = DuiCloseButtonAttributeName)]
public class CloseButtonTagHelper : ButtonTagHelper
{
    private const string DuiCloseButtonAttributeName = "dui-close-button";

    [HtmlAttributeName(DuiCloseButtonAttributeName)]
    public bool CloseButton { get; set; }

    public CloseButtonTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (CloseButton)
        {
            output.Attributes.SetAttribute("x-bind", "closeButton");
        }

        return base.ProcessAsync(context, output);
    }
}
