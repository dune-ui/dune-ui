namespace DuneUI.TagHelpers;

public record AutoFieldConfiguration(
    AutoFieldLayout Layout,
    AutoFieldElement Elements = AutoFieldElement.All
) { };
