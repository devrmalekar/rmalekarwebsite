using Microsoft.Extensions.Caching.Memory;
using Portfolio.Api.Interfaces;

namespace Portfolio.Api.Services;

public class ModeCacheService : IModeCacheService
{
    private readonly IMemoryCache _cache;

    public ModeCacheService(IMemoryCache cache) => _cache = cache;

    public async Task SetTheme(string userKey, string theme)
    {
        _cache.Set(userKey, theme, TimeSpan.FromMinutes(60));
    }

    public async Task<string?> GetTheme(string userKey)
    {
        _cache.TryGetValue<string>(userKey, out string? theme);
        return theme;
    }
}
