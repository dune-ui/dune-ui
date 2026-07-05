using DuneUI.Icons;
using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     A callout that displays a short, important message to the user, optionally with an
///     icon, title, and description.
/// </summary>
[HtmlTargetElement("dui-alert")]
public class AlertTagHelper : DuneUITagHelperBase
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

    private static readonly Dictionary<AlertVariant, ThemeToken> AlertVariantClasses = new()
    {
        [AlertVariant.Default] = new ThemeToken("dui-alert-variant-default"),
        [AlertVariant.Destructive] = new ThemeToken("dui-alert-variant-destructive"),
    };

    /// <summary>
    ///     The descriptive body text of the alert. When set, the alert renders its description
    ///     automatically and child content is not allowed.
    /// </summary>
    [HtmlAttributeName(DescriptionAttributeName)]
    public string? Description { get; set; }

    /// <summary>
    ///     The name of the icon to display in the alert. When set, the alert renders the icon
    ///     automatically and child content is not allowed.
    /// </summary>
    [HtmlAttributeName(IconAttributeName)]
    public string? Icon { get; set; }

    /// <summary>
    ///     The title text of the alert. When set, the alert renders its title automatically and
    ///     child content is not allowed.
    /// </summary>
    [HtmlAttributeName(TitleAttributeName)]
    public string? Title { get; set; }

    /// <summary>
    ///     The visual style of the alert.
    /// </summary>
    /// <remarks>
    ///     Defaults to <see cref="AlertVariant.Default" />.
    /// </remarks>
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
                new ThemeToken("dui-alert"),
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
                    $"Cannot add child content to <dui-alert> when specifying '{TitleAttributeName}', '{DescriptionAttributeName}', or '{IconAttributeName}' attribute."
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
            var iconTagHelper = new IconTagHelper(ThemeManager, ClassMerger, _iconManager)
            {
                Name = Icon,
            };
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
