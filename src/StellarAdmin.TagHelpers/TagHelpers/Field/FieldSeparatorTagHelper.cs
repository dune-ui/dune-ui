using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-field-separator")]
public class FieldSeparatorTagHelper : StellarTagHelper
{
    public FieldSeparatorTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
        : base(themeManager, classMerger) { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        var childContent = await output.GetChildContentAsync();

        output.Attributes.SetAttribute("data-slot", "field-separator");
        output.Attributes.SetAttribute(
            "data-content",
            childContent.IsEmptyOrWhiteSpace ? "false" : "true"
        );
        output.Attributes.SetAttribute(
            "class",
            ClassMerger.Merge(
                new ComponentName("dui-field-separator"),
                "relative",
                output.GetUserSuppliedClass()
            )
        );

        /* Add the actual separator */
        var separatorOutput = new TagHelperOutput(
            "",
            [new TagHelperAttribute("class", "absolute inset-0 top-1/2")],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
        );
        var separatorTagHelper = new SeparatorTagHelper(ThemeManager, ClassMerger)
        {
            Orientation = SeparatorOrientation.Horizontal,
        };
        await separatorTagHelper.ProcessAsync(context, separatorOutput);
        output.Content.AppendHtml(separatorOutput);

        /* Add the child content, if any */
        if (!childContent.IsEmptyOrWhiteSpace)
        {
            var contentWrapperTagBuilder = new TagBuilder("span");
            contentWrapperTagBuilder.Attributes.Add("data-slot", "field-separator-content");
            contentWrapperTagBuilder.Attributes.Add(
                "class",
                ClassMerger.Merge(
                    new ComponentName("dui-field-separator-content"),
                    "bg-background relative mx-auto block w-fit"
                )
            );

            contentWrapperTagBuilder.InnerHtml.AppendHtml(childContent);
            output.Content.AppendHtml(contentWrapperTagBuilder);
        }
    }
}
