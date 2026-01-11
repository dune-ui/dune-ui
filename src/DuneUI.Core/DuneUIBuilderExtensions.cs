using DuneUI.Builders;
using DuneUI.Icons;
using DuneUI.Theming;
using Microsoft.Extensions.DependencyInjection;
using TailwindMerge;

namespace DuneUI;

public static class DuneUIBuilderExtensions
{
    public static DuneUIBuilder AddCore(this DuneUIBuilder builder)
    {
        builder
            .Services.AddSingleton<TwMerge>()
            .AddSingleton<ICssClassMerger, DefaultCssClassMerger>()
            .AddSingleton<IIconManager>(_ => DefaultIconManager.Instance)
            .AddSingleton<ThemeManager>(_ => ThemeManager.Instance);

        return builder;
    }
}
