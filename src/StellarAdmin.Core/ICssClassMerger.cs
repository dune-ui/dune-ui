namespace StellarAdmin;

public interface ICssClassMerger
{
    string? Merge(params string?[] classNames);
}
