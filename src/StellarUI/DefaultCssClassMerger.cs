using TailwindMerge;

namespace StellarUI;

internal class DefaultCssClassMerger(TwMerge twMerge) : ICssClassMerger
{
    public string? Merge(params string?[] classNames)
    {
        return twMerge.Merge(classNames);
    }
}
