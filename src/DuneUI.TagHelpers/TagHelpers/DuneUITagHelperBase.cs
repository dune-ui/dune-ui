using DuneUI.Theming;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DuneUI.TagHelpers;

public class DuneUITagHelperBase : TagHelper
{
    private const string ParentTagHelperStackKey = "duneui-parent-tag-helper-stack";

    private readonly Dictionary<string, TagHelperContent> _namedSlots =
        new Dictionary<string, TagHelperContent>();

    [HtmlAttributeNotBound]
    public ICssClassMerger ClassMerger { get; private set; }

    [HtmlAttributeNotBound]
    protected internal DuneUITagHelperBase? ParentTagHelper { get; private set; }

    [HtmlAttributeNotBound]
    protected ThemeManager ThemeManager { get; }

    public DuneUITagHelperBase(ThemeManager themeManager)
    {
        ThemeManager = themeManager ?? throw new ArgumentNullException(nameof(themeManager));
    }

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

    protected string? BuildClassString(string? componentName, string?[] additionalClasses)
    {
        if (componentName == null)
        {
            return ClassMerger.Merge([.. additionalClasses]);
        }

        return ClassMerger.Merge(
            [ThemeManager.GetComponentClass(componentName), .. additionalClasses]
        );
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
