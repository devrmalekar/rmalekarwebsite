namespace Portfolio.Shared.Dtos;

public class CacheEntry<T>
{
    public T? Data { get; set; }
    public DateTime Expiry { get; set; }

    public bool IsExpired => DateTime.UtcNow > Expiry;
}
