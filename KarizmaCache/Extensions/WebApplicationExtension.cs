using KarizmaPlatform.Cache.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace KarizmaPlatform.Cache.Extensions;

public static class WebApplicationExtension
{
    public static async Task LoadCaches(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var caches = scope.ServiceProvider.GetServices<ICache>();

        foreach (var cache in caches)
            await cache.ReloadCache();
    }
}