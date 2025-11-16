using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StellarUI.TagHelpers;

public class StellarTagHelper : TagHelper
{
    private readonly Dictionary<string, TagHelperContent> _namedSlots = new();
    private const string ParentTagHelperStackKey = "stellar-parent-tag-helper-stack";

    [HtmlAttributeNotBound]
    protected internal StellarTagHelper? ParentTagHelper { get; private set; }

    private Stack<StellarTagHelper> GetParentTagHelperStack(TagHelperContext context)
    {
        if (
            context.Items.TryGetValue(ParentTagHelperStackKey, out var stack)
            && (stack is Stack<StellarTagHelper> parentTagHelperStack)
        )
        {
            return parentTagHelperStack;
        }

        parentTagHelperStack = new Stack<StellarTagHelper>();
        context.Items[ParentTagHelperStackKey] = parentTagHelperStack;

        return parentTagHelperStack;
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

    public bool TryGetNamedSlot(string name, out TagHelperContent? content)
    {
        return _namedSlots.TryGetValue(name, out content);
    }

    public bool TryAddNamedSlot(string name, TagHelperContent childContent)
    {
        return _namedSlots.TryAdd(name, childContent);
    }
}
