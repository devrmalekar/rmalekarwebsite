using Microsoft.Extensions.Caching.Memory;

namespace Portfolio.Api.Interfaces;

public interface IModeCacheService
{
    Task SetTheme(string userKey, string theme);

    Task<string?> GetTheme(string userKey);
}
