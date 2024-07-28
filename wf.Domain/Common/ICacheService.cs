namespace wf.Domain.Common;

public interface ICacheService
{
    T? Get<T>(string key);
    Task<T?> GetOrSet<T>(string key, Func<Task<T>> resolver, TimeSpan? expireIn = null, bool refresh = false);
    void Set<T>(string key, T value, TimeSpan? expireIn);
    void Remove(string key);
}
