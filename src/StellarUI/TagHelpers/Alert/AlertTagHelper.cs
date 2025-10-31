using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

[HtmlTargetElement("sui-alert")]
public class AlertTagHelper(ICssClassMerger classMerger) : StellarTagHelper
{
    private const string DescriptionAttributeName = "description";
    private const string IconAttributeName = "icon";
    private const string TitleAttributeName = "title";

    private static readonly Dictionary<AlertVariant, string> AlertVariantClasses = new()
    {
        [AlertVariant.Default] = "bg-card text-card-foreground",
        [AlertVariant.Information] =
            "text-information bg-card [&>svg]:text-current *:data-[slot=alert-description]:text-information/90",
        [AlertVariant.Warning] =
            "text-warning bg-card [&>svg]:text-current *:data-[slot=alert-description]:text-warning/90",
        [AlertVariant.Success] =
            "text-success bg-card [&>svg]:text-current *:data-[slot=alert-description]:text-success/90",
        [AlertVariant.Destructive] =
            "text-destructive bg-card [&>svg]:text-current *:data-[slot=alert-description]:text-destructive/90",
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
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        var effectiveVariant = Variant ?? AlertVariant.Default;

        output.Attributes.SetAttribute("data-slot", "alert");
        output.Attributes.SetAttribute("role", "alert");
        output.Attributes.SetAttribute(
            "class",
            classMerger.Merge(
                "relative w-full rounded-lg border px-4 py-3 text-sm grid has-[>svg]:grid-cols-[calc(var(--spacing)*4)_1fr] grid-cols-[0_1fr] has-[>svg]:gap-x-3 gap-y-0.5 items-start [&>svg]:size-4 [&>svg]:translate-y-0.5 [&>svg]:text-current",
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
                    $"Cannot add child content to <sui-alert> when specifying '{TitleAttributeName}', '{DescriptionAttributeName}', or '{IconAttributeName}' attribute."
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
            var iconTagHelper = new IconTagHelper { Name = Icon };
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
            var titleTagHelper = new AlertTitleTagHelper(classMerger);
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

            var descriptionTagHelper = new AlertDescriptionTagHelper(classMerger);
            await descriptionTagHelper.ProcessAsync(context, descriptionTagHelperOutput);

            output.Content.AppendHtml(descriptionTagHelperOutput);
        }
    }
}
