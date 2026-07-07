namespace SkillsGenerator;

/// <summary>A public enum discovered in the tag-helper sources.</summary>
internal sealed record EnumInfo(string Name, IReadOnlyList<EnumMember> Members);

/// <summary>A single enum member and its <c>&lt;summary&gt;</c> doc-comment text.</summary>
internal sealed record EnumMember(string Name, string Summary);

/// <summary>A bound attribute on a tag-helper class.</summary>
internal sealed record AttributeInfo(string Name, string TypeText, string Summary, string Default);

/// <summary>A single tag (<c>dui-...</c>) rendered by a tag-helper class.</summary>
internal sealed record TagInfo(
    string Name,
    string Summary,
    IReadOnlyList<AttributeInfo> Attributes
);

/// <summary>A usage snippet surfaced in a component's reference file, with its source label.</summary>
internal sealed record ExampleInfo(string Snippet, string Source);

/// <summary>A component = one folder under <c>TagHelpers/</c> exposing one or more tags.</summary>
internal sealed record ComponentInfo(
    string FolderName,
    IReadOnlyList<TagInfo> Tags,
    TagInfo Primary,
    IReadOnlyList<ExampleInfo> Examples
);
