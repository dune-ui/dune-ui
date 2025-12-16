using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarAdmin.TagHelpers;

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
}
