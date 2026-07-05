namespace DuneUI.TagHelpers;

/// <summary>
///     Describes how a field is automatically generated — its layout and which sub-elements
///     to render.
/// </summary>
/// <param name="Layout">The layout used when rendering the field.</param>
/// <param name="Elements">
///     Which field elements (label, input, description, etc.) to render. Defaults to
///     <see cref="AutoFieldElement.All" />.
/// </param>
public record AutoFieldConfiguration(
    AutoFieldLayout Layout,
    AutoFieldElement Elements = AutoFieldElement.All
) { };
