using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Wrappers.MemoryCache;

public class MemoryCacheWrapper
{
	private readonly IMemoryCache _memoryCache;

	public MemoryCacheWrapper(IMemoryCache memoryCache) => _memoryCache = memoryCache;

	public virtual async Task<TItem> GetOrCreateAsync<TItem>(object key, Func<ICacheEntry, Task<TItem>> factory)
	{
		if (!TryGetValue(key, out TItem result))
		{
			result = await _memoryCache.GetOrCreateAsync(key, factory);
		}

		return result;
	}

	public virtual bool TryGetValue<T>(object Key, out T cache)
	{
		if (_memoryCache.TryGetValue(Key, out T cachedItem))
		{
			cache = cachedItem;
			return true;
		}
		cache = default;
		return false;
	}
}
