using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

public static class TagHelperOutputExtensions
{
    public static string GetUserSuppliedClass(this TagHelperOutput output)
    {
        if (
            output.Attributes.ContainsName("class")
            && output.Attributes["class"].Value?.ToString() is { } userSpecifiedClass
        )
        {
            return userSpecifiedClass;
        }

        return string.Empty;
    }

    /// <summary>
    ///     Appends a declaration to the element's <c>style</c> attribute, preserving any
    ///     existing (e.g. user-supplied) value.
    /// </summary>
    public static void AppendStyle(this TagHelperOutput output, string declaration)
    {
        var existing =
            output.Attributes.ContainsName("style")
            && output.Attributes["style"].Value?.ToString() is { } value
                ? value.TrimEnd().TrimEnd(';')
                : null;

        output.Attributes.SetAttribute(
            "style",
            string.IsNullOrEmpty(existing) ? declaration : $"{existing}; {declaration}"
        );
    }
}
