using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Icons;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

[HtmlTargetElement("sa-alert")]
public class AlertTagHelper : StellarTagHelper
{
    private readonly IIconManager _iconManager;

    public AlertTagHelper(
        ThemeManager themeManager,
        ICssClassMerger classMerger,
        IIconManager iconManager
    )
        : base(themeManager, classMerger)
    {
        _iconManager = iconManager ?? throw new ArgumentNullException(nameof(iconManager));
    }

    private const string DescriptionAttributeName = "description";
    private const string IconAttributeName = "icon";
    private const string TitleAttributeName = "title";

    private static readonly Dictionary<AlertVariant, ComponentName> AlertVariantClasses = new()
    {
        [AlertVariant.Default] = new ComponentName("dui-alert-variant-default"),
        [AlertVariant.Destructive] = new ComponentName("dui-alert-variant-destructive"),
    };

    [HtmlAttributeName(DescriptionAttributeName)]
    public string? Description { get; set; }

    [HtmlAttributeName(IconAttributeName)]
    public string? Icon { get; set; }

    [HtmlAttributeName(TitleAttributeName)]
    public string? Title { get; set; }

    [HtmlAttributeName("variant")]
    public AlertVariant? Variant { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var effectiveVariant = Variant ?? AlertVariant.Default;

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("data-slot", "alert");
        output.Attributes.SetAttribute("role", "alert");
        output.Attributes.SetAttribute(
            "class",
            BuildClassString(
                new ComponentName("dui-alert"),
                "w-full relative group/alert",
                AlertVariantClasses[effectiveVariant],
                output.GetUserSuppliedClass()
            )
        );

        var childContent = await output.GetChildContentAsync();

        if (
            !string.IsNullOrEmpty(Title)
            || !string.IsNullOrEmpty(Description)
            || !string.IsNullOrEmpty(Icon)
        )
        {
            if (!childContent.IsEmptyOrWhiteSpace)
            {
                throw new Exception(
                    $"Cannot add child content to <sa-alert> when specifying '{TitleAttributeName}', '{DescriptionAttributeName}', or '{IconAttributeName}' attribute."
                );
            }

            await RenderImplicitChildContent(context, output);
        }
        else
        {
            output.Content.SetHtmlContent(childContent);
        }
    }

    private async Task RenderImplicitChildContent(TagHelperContext context, TagHelperOutput output)
    {
        if (!string.IsNullOrEmpty(Icon))
        {
            var iconTagHelperOutput = new TagHelperOutput(
                string.Empty,
                [],
                (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
            );
            var iconTagHelper = new IconTagHelper(_iconManager) { Name = Icon };
            await iconTagHelper.ProcessAsync(context, iconTagHelperOutput);

            output.Content.AppendHtml(iconTagHelperOutput);
        }

        if (!string.IsNullOrEmpty(Title))
        {
            var titleContent = new DefaultTagHelperContent();
            titleContent.Append(Title);

            var titleTagHelperOutput = new TagHelperOutput(
                string.Empty,
                [],
                (_, _) => Task.FromResult<TagHelperContent>(titleContent)
            );
            var titleTagHelper = new AlertTitleTagHelper(ThemeManager, ClassMerger);
            await titleTagHelper.ProcessAsync(context, titleTagHelperOutput);

            output.Content.AppendHtml(titleTagHelperOutput);
        }

        if (!string.IsNullOrEmpty(Description))
        {
            var descriptionContent = new DefaultTagHelperContent();
            descriptionContent.Append(Description);

            var descriptionTagHelperOutput = new TagHelperOutput(
                string.Empty,
                [],
                (_, _) => Task.FromResult<TagHelperContent>(descriptionContent)
            );

            var descriptionTagHelper = new AlertDescriptionTagHelper(ThemeManager, ClassMerger);
            await descriptionTagHelper.ProcessAsync(context, descriptionTagHelperOutput);

            output.Content.AppendHtml(descriptionTagHelperOutput);
        }
    }
}
