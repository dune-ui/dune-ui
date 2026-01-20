using DuneUI.Theming;

namespace DuneUI;

public interface ICssClassMerger
{
    string? Merge(params ClassElement?[] classes);
}
