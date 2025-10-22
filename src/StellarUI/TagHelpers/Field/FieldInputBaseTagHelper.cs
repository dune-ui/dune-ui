using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

public abstract class FieldInputBaseTagHelper(
    IStellarHtmlGenerator htmlGenerator,
    ICssClassMerger classMerger
) : StellarTagHelper
{
    private const string ForAttributeName = "asp-for";

    [HtmlAttributeName("description")]
    public string? Description { get; set; }

    [HtmlAttributeName("error")]
    public string? Error { get; set; }

    /// <summary>
    ///     An expression to be evaluated against the current model.
    /// </summary>
    [HtmlAttributeName(ForAttributeName)]
    public ModelExpression? For { get; set; }

    [HtmlAttributeName("label")]
    public string? Label { get; set; }

    /// <summary>
    ///     The name of the &lt;input&gt; element.
    /// </summary>
    /// <remarks>
    ///     Passed through to the generated HTML in all cases. Also used to determine whether <see cref="For" /> is
    ///     valid with an empty <see cref="ModelExpression.Name" />.
    /// </remarks>
    public string? Name { get; set; }

    [HtmlAttributeName("render-field")]
    public bool? ShouldRenderField { get; set; }

    /// <summary>
    ///     Gets the <see cref="ViewContext" /> of the executing view.
    /// </summary>
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (ShouldRenderFieldWrapper())
        {
            await RenderFieldWrapper(context, output);
        }
        else
        {
            await InternalRenderInput(context, output);
        }
    }

    internal virtual AutoFieldLayout GetAutoFieldLayout()
    {
        return AutoFieldLayout.Vertical;
    }

    protected abstract Task RenderInput(
        TagHelperContext context,
        TagHelperOutput output,
        IDictionary<string, object?>? htmlAttributes
    );

    private async Task InternalRenderInput(TagHelperContext context, TagHelperOutput output)
    {
        if (Name != null)
        {
            output.CopyHtmlAttribute(nameof(Name), context);
        }

        IDictionary<string, object?>? htmlAttributes = null;
        if (
            string.IsNullOrEmpty(For?.Name)
            && string.IsNullOrEmpty(ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix)
            && !string.IsNullOrEmpty(Name)
        )
        {
            htmlAttributes = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase)
            {
                { "name", Name },
            };
        }

        await RenderInput(context, output, htmlAttributes);
    }

    private async Task RenderDescriptionControl(TagHelperOutput parentOutput)
    {
        if (For != null || Description != null)
        {
            var descriptionTagHelperOutput = new TagHelperOutput(
                string.Empty,
                [],
                (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
            );
            var fieldDescriptionRenderer = new FieldDescriptionRenderer(classMerger);
            await fieldDescriptionRenderer.Render(descriptionTagHelperOutput, For, Description);

            parentOutput.Content.AppendHtml(descriptionTagHelperOutput);
        }
    }

    private async Task RenderFieldWrapper(TagHelperContext context, TagHelperOutput output)
    {
        var wrappedOutput = new TagHelperOutput(
            string.Empty,
            [],
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
        );

        var autoFieldLayout = GetAutoFieldLayout();
        if (
            autoFieldLayout
            is AutoFieldLayout.HorizontalInputFirst
                or AutoFieldLayout.HorizontalInputLast
        )
        {
            if (autoFieldLayout == AutoFieldLayout.HorizontalInputFirst)
            {
                await RenderInputControl(context, [.. output.Attributes], wrappedOutput);
            }

            var fieldContentOutput = new TagHelperOutput(
                string.Empty,
                [],
                (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
            );

            var fieldContentRenderer = new FieldContentRenderer(classMerger);
            await fieldContentRenderer.Render(fieldContentOutput);

            await RenderLabelControl(fieldContentOutput);
            await RenderDescriptionControl(fieldContentOutput);
            await RenderErrorControl(wrappedOutput);

            wrappedOutput.Content.AppendHtml(fieldContentOutput);

            if (autoFieldLayout == AutoFieldLayout.HorizontalInputLast)
            {
                await RenderInputControl(context, [.. output.Attributes], wrappedOutput);
            }
        }
        else
        {
            await RenderLabelControl(wrappedOutput);
            await RenderInputControl(context, [.. output.Attributes], wrappedOutput);
            await RenderErrorControl(wrappedOutput);
            await RenderDescriptionControl(wrappedOutput);
        }

        /* Render the field */
        output.Attributes.Clear(); // We passed them to the input control

        var fieldRenderer = new FieldRenderer(classMerger);
        await fieldRenderer.Render(
            output,
            autoFieldLayout == AutoFieldLayout.Vertical
                ? FieldOrientation.Vertical
                : FieldOrientation.Horizontal,
            () => Task.FromResult<IHtmlContent>(wrappedOutput)
        );

        output.Content.AppendHtml(wrappedOutput);
    }

    private async Task RenderErrorControl(TagHelperOutput parentOutput)
    {
        if (For != null || Error != null)
        {
            var errorTagHelperOutput = new TagHelperOutput(
                string.Empty,
                [],
                (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
            );
            var renderer = new FieldErrorRenderer(htmlGenerator, classMerger);
            await renderer.Render(errorTagHelperOutput, ViewContext, For, Error);

            parentOutput.Content.AppendHtml(errorTagHelperOutput);
        }
    }

    private async Task RenderInputControl(
        TagHelperContext context,
        TagHelperAttributeList attributes,
        TagHelperOutput parentOutput
    )
    {
        var inputTagHelperOutput = new TagHelperOutput(
            string.Empty,
            attributes,
            (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
        );
        await InternalRenderInput(context, inputTagHelperOutput);

        parentOutput.Content.AppendHtml(inputTagHelperOutput);
    }

    private async Task RenderLabelControl(TagHelperOutput parentOutput)
    {
        if (For != null || Label != null)
        {
            var labelTagHelperOutput = new TagHelperOutput(
                string.Empty,
                [],
                (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
            );
            var fieldLabelRenderer = new FieldLabelRenderer(htmlGenerator, classMerger);
            await fieldLabelRenderer.Render(labelTagHelperOutput, ViewContext, For, Label);

            parentOutput.Content.AppendHtml(labelTagHelperOutput);
        }
    }

    private bool ShouldRenderFieldWrapper()
    {
        // It the user explicitly indicated whether we should render a field, then we honor that
        if (ShouldRenderField.HasValue)
        {
            return ShouldRenderField.Value;
        }

        // If we're rendering inside a field tag helper, we don't render another one
        if (GetParentTagHelper<FieldTagHelper>() != null)
        {
            return false;
        }

        // If any of the explicit field attributes or the asp-for attribute is set, we will render a field
        if (For != null || Label != null || Description != null || Error != null)
        {
            return true;
        }

        // If none of the above conditions are true, do not render a field
        return false;
    }
}
