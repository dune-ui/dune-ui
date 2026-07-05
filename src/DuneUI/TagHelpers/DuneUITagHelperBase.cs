using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

/// <summary>
///     Base class for all DuneUI tag helpers. Provides the theme manager and CSS class
///     merger, named-slot support, and tracks the ancestor tag-helper stack so children can
///     locate their parents.
/// </summary>
public class DuneUITagHelperBase : TagHelper
{
    private const string ParentTagHelperStackKey = "duneui-parent-tag-helper-stack";

    private readonly Dictionary<string, TagHelperContent> _namedSlots =
        new Dictionary<string, TagHelperContent>();

    /// <summary>
    ///     The CSS class merger used to compose and de-duplicate Tailwind utility classes.
    /// </summary>
    [HtmlAttributeNotBound]
    public ICssClassMerger ClassMerger { get; }

    /// <summary>
    ///     The immediate ancestor DuneUI tag helper, or <c>null</c> when this is a root.
    /// </summary>
    [HtmlAttributeNotBound]
    protected internal DuneUITagHelperBase? ParentTagHelper { get; private set; }

    /// <summary>
    ///     The theme manager that resolves component classes from the active theme pack.
    /// </summary>
    [HtmlAttributeNotBound]
    protected ThemeManager ThemeManager { get; }

    public DuneUITagHelperBase(ThemeManager themeManager, ICssClassMerger classMerger)
    {
        ThemeManager = themeManager ?? throw new ArgumentNullException(nameof(themeManager));
        ClassMerger = classMerger ?? throw new ArgumentNullException(nameof(classMerger));
    }

    public override void Init(TagHelperContext context)
    {
        var parentStack = GetParentTagHelperStack(context);

        // Get the current parent, if any
        ParentTagHelper = parentStack.Count == 0 ? null : parentStack.Peek();

        // Push the current component to the stack (if not a slot)
        if (this is not SlotTagHelper)
        {
            parentStack.Push(this);
        }
    }

    public bool TryAddNamedSlot(string name, TagHelperContent childContent)
    {
        return _namedSlots.TryAdd(name, childContent);
    }

    public bool TryGetNamedSlot(string name, out TagHelperContent? content)
    {
        return _namedSlots.TryGetValue(name, out content);
    }

    protected string BuildClassString(params ClassElement[] classes)
    {
        return ClassMerger.Merge(classes) ?? string.Empty;
    }

    protected string? BuildClassString(string? themeTokenName, string?[] additionalClasses)
    {
        if (themeTokenName == null)
        {
            return ClassMerger.Merge([.. additionalClasses]);
        }

        return ClassMerger.Merge([
            ThemeManager.GetComponentClass(themeTokenName),
            .. additionalClasses,
        ]);
    }

    /// <summary>
    ///     Publishes a context object that descendant tag helpers can read with
    ///     <see cref="GetContext{T}" />. The value is keyed by its type and stored in
    ///     <see cref="TagHelperContext.Items" />, so it must be set before the parent renders its
    ///     children (i.e. before <c>GetChildContentAsync</c>).
    /// </summary>
    protected void SetContext<T>(TagHelperContext context, T value)
        where T : class
    {
        context.Items[typeof(T)] = value;
    }

    /// <summary>
    ///     Reads a context object published by an ancestor tag helper with
    ///     <see cref="SetContext{T}" />, or <c>null</c> if no ancestor published one.
    /// </summary>
    protected T? GetContext<T>(TagHelperContext context)
        where T : class
    {
        return context.Items.TryGetValue(typeof(T), out var value) ? value as T : null;
    }

    protected T? GetParentTagHelper<T>()
        where T : DuneUITagHelperBase
    {
        var currentParentTagHelper = ParentTagHelper;
        while (currentParentTagHelper is not null)
        {
            if (currentParentTagHelper is T asT)
            {
                return asT;
            }

            currentParentTagHelper = currentParentTagHelper.ParentTagHelper;
        }

        return null;
    }

    protected string? GetUserSpecifiedClass(TagHelperOutput output)
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

    private Stack<DuneUITagHelperBase> GetParentTagHelperStack(TagHelperContext context)
    {
        if (
            context.Items.TryGetValue(ParentTagHelperStackKey, out var stack)
            && stack is Stack<DuneUITagHelperBase> parentTagHelperStack
        )
        {
            return parentTagHelperStack;
        }

        parentTagHelperStack = new Stack<DuneUITagHelperBase>();
        context.Items[ParentTagHelperStackKey] = parentTagHelperStack;

        return parentTagHelperStack;
    }
}
