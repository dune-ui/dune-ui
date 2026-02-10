using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI;

public interface IAttributeMerger
{
    Task<TagHelperContent> MergeAttributes(
        TagHelperContent content,
        IEnumerable<TagHelperAttribute> attributes
    );
}
