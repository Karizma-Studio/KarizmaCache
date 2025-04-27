using KarizmaPlatform.Cache.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace KarizmaPlatform.Cache.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddCaches(this IServiceCollection services)
    {
        var cacheType = typeof(ICache);
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var caches = assemblies.SelectMany(a => a.GetTypes())
            .Where(t => cacheType.IsAssignableFrom(t) && !t.IsAbstract);

        foreach (var cache in caches)
        {
            services.AddSingleton(cache);
            services.AddSingleton<ICache>(sp => (ICache)sp.GetRequiredService(cache));
        }

        return services;
    }
}