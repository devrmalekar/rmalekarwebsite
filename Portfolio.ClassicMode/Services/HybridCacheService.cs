using Portfolio.ClassicMode.IServices;

namespace Portfolio.ClassicMode.Services;

public enum DataSOurce
{
    Memory, //custom cache storage
    LocalStorage, //blazorwasm local storage
    Api,
}

public class HybridCacheService
{
    private readonly IMemoryCache _memory;
    private readonly LocalStorageCacheService _local;

    public HybridCacheService(IMemoryCache memory, LocalStorageCacheService local)
    {
        _memory = memory;
        _local = local;
    }

    public async Task<(DataSOurce Source, T value)> GetAsync<T>(
        string key,
        Func<Task<T>> apiCall,
        TimeSpan duration
    )
    {
        //1. Try memory
        if (_memory.TryGet<T>(key, out var mem))
        {
            return (DataSOurce.Memory, mem);
        }

        //2. Try LocalStorage
        var local = await _local.TryGetAsync<T>(key);
        if (local != null)
        {
            _memory.Set(key, local, duration);
            return (DataSOurce.LocalStorage, local);
        }

        //3. fetch from API
        var api = await apiCall();
        //Console.WriteLine(api);
        _memory.Set(key, api, duration);
        await _local.SetAsync(key, api, duration);

        return (DataSOurce.Api, api);
    }

    public async Task ClearAsync()
    {
        _memory.Clear();
        await _local.ClearAsync();
    }

    public async Task RemoveAsync(string key)
    {
        _memory.Remove(key);
        await _local.RemoveAsync(key);
    }
}
