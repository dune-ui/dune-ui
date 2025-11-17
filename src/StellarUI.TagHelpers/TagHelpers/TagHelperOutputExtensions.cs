using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

internal static class TagHelperOutputExtensions
{
    public static string GetContent(this TagHelperOutput output)
    {
        using var writer = new StringWriter();

        output.WriteTo(writer, HtmlEncoder.Default);

        return writer.ToString();
    }

    public static string? GetUserSuppliedClass(this TagHelperOutput output)
    {
        if (
            output.Attributes.ContainsName("class")
            && output.Attributes["class"].Value?.ToString() is { } userSpecifiedClass
        )
        {
            return userSpecifiedClass;
        }

        return null;
    }
}
