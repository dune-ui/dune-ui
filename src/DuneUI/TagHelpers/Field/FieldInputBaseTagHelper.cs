using DuneUI.Theming;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

public abstract class FieldInputBaseTagHelper : DuneUITagHelperBase
{
    private readonly IHtmlGenerator _htmlGenerator;

    protected FieldInputBaseTagHelper(
        ThemeManager themeManager,
        IHtmlGenerator htmlGenerator,
        ICssClassMerger classMerger
    )
        : base(themeManager, classMerger)
    {
        _htmlGenerator = htmlGenerator ?? throw new ArgumentNullException(nameof(htmlGenerator));
    }

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
    public required ViewContext ViewContext { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var autoFieldLayout = await InternalRenderInput(context, output);

        if (ShouldRenderFieldWrapper())
        {
            await RenderFieldWrapper(context, output, autoFieldLayout);
        }
    }

    protected abstract Task<AutoFieldConfiguration> RenderInput(
        TagHelperContext context,
        TagHelperOutput output,
        IDictionary<string, object?>? htmlAttributes
    );

    private async Task<AutoFieldConfiguration> InternalRenderInput(
        TagHelperContext context,
        TagHelperOutput output
    )
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

        return await RenderInput(context, output, htmlAttributes);
    }

    private async Task RenderDescriptionControl(
        TagHelperContext context,
        TagHelperContent targetContent,
        AutoFieldElement elements
    )
    {
        if (
            (For != null || Description != null)
            && elements.HasFlagFast(AutoFieldElement.Description)
        )
        {
            var descriptionTagHelperOutput = new TagHelperOutput(
                string.Empty,
                [],
                (_, _) =>
                    Description == null
                        ? Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
                        : Task.FromResult(new DefaultTagHelperContent().Append(Description))
            );
            var fieldDescriptionTagHelper = new FieldDescriptionTagHelper(ThemeManager, ClassMerger)
            {
                For = For,
                ViewContext = ViewContext,
            };
            await fieldDescriptionTagHelper.ProcessAsync(context, descriptionTagHelperOutput);

            targetContent.AppendHtml(descriptionTagHelperOutput);
        }
    }

    private async Task RenderFieldWrapper(
        TagHelperContext context,
        TagHelperOutput output,
        AutoFieldConfiguration autoFieldConfiguration
    )
    {
        var fieldTagBuilder = new FieldTagBuilder(
            ClassMerger,
            autoFieldConfiguration.Layout == AutoFieldLayout.Vertical
                ? FieldOrientation.Vertical
                : FieldOrientation.Horizontal,
            null
        );

        // Render the opening tag of the Field wrapper
        output.PreElement.AppendHtml(fieldTagBuilder.RenderStartTag());

        if (
            autoFieldConfiguration.Layout
            is AutoFieldLayout.HorizontalInputFirst
                or AutoFieldLayout.HorizontalInputLast
        )
        {
            // Render the label and description inside a field content, and also render the error
            TagHelperContent targetContent =
                autoFieldConfiguration.Layout == AutoFieldLayout.HorizontalInputFirst
                    ? output.PostElement
                    : output.PreElement;

            var fieldContentOutput = new TagHelperOutput(
                string.Empty,
                [],
                (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
            );

            var fieldContentTagHelper = new FieldContentTagHelper(ThemeManager, ClassMerger);
            await fieldContentTagHelper.ProcessAsync(context, fieldContentOutput);

            await RenderLabelControl(
                context,
                fieldContentOutput.Content,
                autoFieldConfiguration.Elements
            );
            await RenderDescriptionControl(
                context,
                fieldContentOutput.Content,
                autoFieldConfiguration.Elements
            );
            await RenderErrorControl(
                context,
                fieldContentOutput.Content,
                autoFieldConfiguration.Elements
            );

            targetContent.AppendHtml(fieldContentOutput);
        }
        else
        {
            await RenderLabelControl(context, output.PreElement, autoFieldConfiguration.Elements);
            await RenderErrorControl(context, output.PostElement, autoFieldConfiguration.Elements);
            await RenderDescriptionControl(
                context,
                output.PostElement,
                autoFieldConfiguration.Elements
            );
        }

        // Render the closing tag of the field wrapper
        output.PostElement.AppendHtml(fieldTagBuilder.RenderEndTag());
    }

    private async Task RenderErrorControl(
        TagHelperContext context,
        TagHelperContent targetContent,
        AutoFieldElement elements
    )
    {
        if ((For != null || Error != null) && elements.HasFlag(AutoFieldElement.Error))
        {
            var errorTagHelperOutput = new TagHelperOutput(
                string.Empty,
                [],
                (_, _) =>
                    Error == null
                        ? Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
                        : Task.FromResult(new DefaultTagHelperContent().Append(Error))
            );
            var fieldErrorTagHelper = new FieldErrorTagHelper(
                ThemeManager,
                _htmlGenerator,
                ClassMerger
            )
            {
                For = For,
                ViewContext = ViewContext,
            };
            await fieldErrorTagHelper.ProcessAsync(context, errorTagHelperOutput);

            targetContent.AppendHtml(errorTagHelperOutput);
        }
    }

    private async Task RenderLabelControl(
        TagHelperContext context,
        TagHelperContent targetContent,
        AutoFieldElement elements
    )
    {
        if ((For != null || Label != null) && elements.HasFlagFast(AutoFieldElement.Label))
        {
            var labelTagHelperOutput = new TagHelperOutput(
                string.Empty,
                [],
                (_, _) =>
                    Label == null
                        ? Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
                        : Task.FromResult(new DefaultTagHelperContent().Append(Label))
            );
            var fieldLabelTagHelper = new FieldLabelTagHelper(
                ThemeManager,
                _htmlGenerator,
                ClassMerger
            )
            {
                For = For,
                ViewContext = ViewContext,
            };
            await fieldLabelTagHelper.ProcessAsync(context, labelTagHelperOutput);

            targetContent.AppendHtml(labelTagHelperOutput);
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
