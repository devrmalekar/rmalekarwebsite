using Portfolio.ClassicMode.IServices;
using Portfolio.Shared.Dtos;

namespace Portfolio.ClassicMode.Services;

public class MemoryCacheService : IMemoryCache
{
    private readonly Dictionary<string, object> _cache = new();

    public bool TryGet<T>(string key, out T value)
    {
        if (_cache.TryGetValue(key, out var obj) && obj is CacheEntry<T> entry)
        {
            if (!entry.IsExpired)
            {
                value = entry.Data!;
                return true;
            }
            _cache.Remove(key);
        }

        value = default!;
        return false;
    }

    public void Set<T>(string key, T data, TimeSpan duration)
    {
        _cache[key] = new CacheEntry<T> { Data = data, Expiry = DateTime.UtcNow.Add(duration) };
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }

    public void Clear()
    {
        _cache.Clear();
    }
}
