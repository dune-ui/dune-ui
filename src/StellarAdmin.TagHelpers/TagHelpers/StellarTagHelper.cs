using Microsoft.AspNetCore.Razor.TagHelpers;
using StellarAdmin.Theming;

namespace StellarAdmin.TagHelpers;

public class StellarTagHelper : TagHelper
{
    private const string ParentTagHelperStackKey = "stellar-parent-tag-helper-stack";

    private readonly Dictionary<string, TagHelperContent> _namedSlots =
        new Dictionary<string, TagHelperContent>();

    [HtmlAttributeNotBound]
    public ICssClassMerger ClassMerger { get; private set; }

    [HtmlAttributeNotBound]
    protected internal StellarTagHelper? ParentTagHelper { get; private set; }

    [HtmlAttributeNotBound]
    protected ThemeManager ThemeManager { get; }

    public StellarTagHelper(ThemeManager themeManager)
    {
        ThemeManager = themeManager ?? throw new ArgumentNullException(nameof(themeManager));
    }

    public StellarTagHelper(ThemeManager themeManager, ICssClassMerger classMerger)
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
        return ClassMerger.Merge(
                classes
                    .Select(c =>
                    {
                        return c switch
                        {
                            ComponentName cn => ThemeManager.GetComponentClass(cn.Name),
                            ClassList cl => cl.Classes,
                            _ => string.Empty,
                        };
                    })
                    .ToArray()
            ) ?? string.Empty;
    }

    protected string? BuildClassString(string? componentName, string?[] additionalClasses)
    {
        if (componentName == null)
        {
            return ClassMerger.Merge(additionalClasses);
        }

        return ClassMerger.Merge(
            [ThemeManager.GetComponentClass(componentName), .. additionalClasses]
        );
    }

    protected T? GetParentTagHelper<T>()
        where T : StellarTagHelper
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

    private Stack<StellarTagHelper> GetParentTagHelperStack(TagHelperContext context)
    {
        if (
            context.Items.TryGetValue(ParentTagHelperStackKey, out var stack)
            && stack is Stack<StellarTagHelper> parentTagHelperStack
        )
        {
            return parentTagHelperStack;
        }

        parentTagHelperStack = new Stack<StellarTagHelper>();
        context.Items[ParentTagHelperStackKey] = parentTagHelperStack;

        return parentTagHelperStack;
    }
}

public abstract record ClassElement
{
    public static implicit operator ClassElement(string? classes)
    {
        if (classes == null)
        {
            return new ClassList(string.Empty);
        }

        return new ClassList(classes);
    }
}

public record ClassList(string Classes) : ClassElement;

public record ComponentName(string Name) : ClassElement;
