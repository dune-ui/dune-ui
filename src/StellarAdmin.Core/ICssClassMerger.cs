using StellarAdmin.Theming;

namespace StellarAdmin;

public interface ICssClassMerger
{
    string? Merge(params ClassElement?[] classes);
}
