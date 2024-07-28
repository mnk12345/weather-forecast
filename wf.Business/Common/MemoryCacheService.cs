using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using wf.Domain.Common;

namespace wf.Business.Common;

public class MemoryCacheService(IMemoryCache memoryCache) : ICacheService
{
    public T? Get<T>(string key)
    {
        memoryCache.TryGetValue(key, out string? stringValue);

        return stringValue != null ? JsonSerializer.Deserialize<T>(stringValue) : default;
    }

    public async Task<T?> GetOrSet<T>(string key, Func<Task<T>> resolver, TimeSpan? expireIn = null, bool refresh = false)
    {
        if (memoryCache.Get(key) is string stringValue && !refresh)
        {
            return JsonSerializer.Deserialize<T>(stringValue);
        }

        var item = await resolver();
        if (item is not null)
        {
            Set(key, item, expireIn);
        }

        return item;
    }

    public void Set<T>(string key, T value, TimeSpan? expireIn)
    {
        if (expireIn is null)
        {
            memoryCache.Set(key, JsonSerializer.Serialize(value));
        }
        else
        {
            memoryCache.Set(key, JsonSerializer.Serialize(value), expireIn.Value);
        }
    }

    public void Remove(string key)
    {
        memoryCache.Remove(key);
    }
}
