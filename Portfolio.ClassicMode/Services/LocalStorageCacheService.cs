using Blazored.LocalStorage;
using Portfolio.ClassicMode.IServices;
using Portfolio.Shared.Dtos;

namespace Portfolio.ClassicMode.Services;

public class LocalStorageCacheService
{
    private readonly ILocalStorageService _localStorage;

    public LocalStorageCacheService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<T?> TryGetAsync<T>(string key)
    {
        var entry = await _localStorage.GetItemAsync<CacheEntry<T>>(key);

        if (entry == null)
        {
            return default;
        }

        if (entry.IsExpired)
        {
            await _localStorage.RemoveItemAsync(key);
            return default;
        }

        return entry.Data;
    }

    public async Task SetAsync<T>(string key, T data, TimeSpan duration)
    {
        var entry = new CacheEntry<T> { Data = data, Expiry = DateTime.UtcNow.Add(duration) };

        await _localStorage.SetItemAsync(key, entry);
    }

    public async Task ClearAsync()
    {
        await _localStorage.ClearAsync();
    }

    public async Task RemoveAsync(string key)
    {
        await _localStorage.RemoveItemAsync(key);
    }
}
