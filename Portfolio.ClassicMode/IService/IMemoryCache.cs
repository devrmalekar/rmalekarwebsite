namespace Portfolio.ClassicMode.IServices;

public interface IMemoryCache
{
    bool TryGet<T>(string key, out T value);

    void Set<T>(string key, T data, TimeSpan duration);

    void Remove(string key);

    void Clear();
}
