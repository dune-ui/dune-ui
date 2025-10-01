namespace StellarUI;

public interface ICssClassMerger
{
    string? Merge(params string?[] classNames);
}
