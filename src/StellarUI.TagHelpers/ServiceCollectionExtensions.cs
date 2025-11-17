using Microsoft.Extensions.DependencyInjection;
using StellarUI.TagHelpers;
using TailwindMerge;

namespace StellarUI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStellarUI(this IServiceCollection services)
    {
        return services
            .AddSingleton<TwMerge>()
            .AddSingleton<ICssClassMerger, DefaultCssClassMerger>()
            .AddSingleton<IStellarHtmlGenerator, DefaultStellarHtmlGenerator>();
    }
}
