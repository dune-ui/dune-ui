using AngleSharp;
using AngleSharp.Io;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI;

internal class AngleSharpAttributeMerger : IAttributeMerger
{
    private readonly IBrowsingContext _browsingContext = BrowsingContext.New(
        Configuration.Default.WithDefaultLoader(
            new LoaderOptions { IsResourceLoadingEnabled = false }
        )
    );

    public async Task<TagHelperContent> MergeAttributes(
        TagHelperContent content,
        IEnumerable<TagHelperAttribute> attributes
    )
    {
        if (content == null)
        {
            throw new ArgumentNullException(nameof(content));
        }

        if (attributes == null)
        {
            throw new ArgumentNullException(nameof(attributes));
        }

        if (content.IsEmptyOrWhiteSpace)
        {
            return content;
        }

        var document = await _browsingContext.OpenAsync(req => req.Content(content.GetContent()));

        if (document.Body == null)
        {
            return content;
        }

        if (document.Body.Children.FirstOrDefault() is not { } firstElement)
        {
            return content;
        }

        foreach (var tagHelperAttribute in attributes)
        {
            firstElement.SetAttribute(
                tagHelperAttribute.Name,
                tagHelperAttribute.Value.ToString() ?? string.Empty
            );
        }

        return new DefaultTagHelperContent().AppendHtml(document.Body.InnerHtml);
    }
}
