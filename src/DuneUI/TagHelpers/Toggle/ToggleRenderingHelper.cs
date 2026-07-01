using DuneUI.Theming;

namespace DuneUI.TagHelpers;

internal static class ToggleRenderingHelper
{
    private static readonly Dictionary<ToggleVariant, ThemeToken> VariantClasses = new Dictionary<
        ToggleVariant,
        ThemeToken
    >
    {
        [ToggleVariant.Default] = new ThemeToken("dui-toggle-variant-default"),
        [ToggleVariant.Outline] = new ThemeToken("dui-toggle-variant-outline"),
    };

    private static readonly Dictionary<ToggleSize, ThemeToken> SizeClasses = new Dictionary<
        ToggleSize,
        ThemeToken
    >
    {
        [ToggleSize.Default] = new ThemeToken("dui-toggle-size-default"),
        [ToggleSize.Small] = new ThemeToken("dui-toggle-size-sm"),
        [ToggleSize.Large] = new ThemeToken("dui-toggle-size-lg"),
    };

    /*
     * The toggle visual is a <label> wrapping an sr-only native <input>. The checked/focus/
     * disabled state therefore lives on the inner input, which is why we read it through
     * has-[:checked] / has-[:focus-visible] / has-[:disabled] here (the dui-toggle token's own
     * checked/focus/validation styles were rewritten to the same has-* forms by the generator).
     */
    private const string BaseLayout =
        "group/toggle inline-flex items-center justify-center whitespace-nowrap shrink-0 select-none cursor-pointer outline-none hover:bg-muted [&_svg]:pointer-events-none [&_svg]:shrink-0 has-[:focus-visible]:ring-3 has-[:disabled]:pointer-events-none has-[:disabled]:opacity-50 has-[:disabled]:cursor-not-allowed";

    /*
     * Group-item-only statics. A grouped toggle item raises the focused item above its
     * neighbours (focus-visible:z-10) so its ring/border isn't clipped by the adjacent item —
     * doubly needed here because joined groups overlap items with -space-x-px. The focus state
     * lives on the inner input, so this is the has-[:focus-visible] form (matching the ring).
     */
    private const string GroupItemLayout = "has-[:focus-visible]:z-10";

    public static string BuildClass(
        ICssClassMerger classMerger,
        ToggleVariant variant,
        ToggleSize size,
        bool includeGroupItemToken,
        string? userClass
    )
    {
        var elements = new List<ClassElement?>
        {
            new ThemeToken("dui-toggle"),
            BaseLayout,
            VariantClasses[variant],
            SizeClasses[size],
        };

        if (includeGroupItemToken)
        {
            elements.Add(GroupItemLayout);
            elements.Add(new ThemeToken("dui-toggle-group-item"));
        }

        // User-supplied class goes last so authoring overrides win in the merge.
        elements.Add(userClass);

        return classMerger.Merge(elements.ToArray()) ?? string.Empty;
    }
}
