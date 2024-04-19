using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings.KeyVault;

internal class Cache : IDisposable
{
    private readonly Dictionary<string, CachedResponse> _cache = new(StringComparer.OrdinalIgnoreCase);

    private SemaphoreSlim? _lock = new(1, 1);

    public void Dispose()
    {
        if (_lock is { })
        {
            _lock.Dispose();
            _lock = null;
        }
    }
    
    internal async ValueTask<Response> GetOrAddAsync(bool isAsync, string uri, TimeSpan ttl,
        Func<ValueTask<Response>> action)
    {
        ThrowIfDisposed();

        if (isAsync)
        {
            await _lock!.WaitAsync().ConfigureAwait(false);
        }
        else
        {
            _lock!.Wait();
        }

        try
        {
            if (_cache.TryGetValue(uri, out CachedResponse cachedResponse) && cachedResponse.IsValid)
            {
                return await cachedResponse.CloneAsync(isAsync).ConfigureAwait(false);
            }

            var response = await action().ConfigureAwait(false);
            if (response is {Status: 200, ContentStream: not null})
            {
                cachedResponse = await CachedResponse.CreateAsync(isAsync, response, ttl).ConfigureAwait(false);
                _cache[uri] = cachedResponse;
            }

            return response;
        }
        finally
        {
            _lock.Release();
        }
    }

    internal void Clear()
    {
        ThrowIfDisposed();

        _lock!.Wait();
        try
        {
            _cache.Clear();
        }
        finally
        {
            _lock.Release();
        }
    }

    private void ThrowIfDisposed()
    {
        if (_lock is null)
        {
            throw new ObjectDisposedException(nameof(_lock));
        }
    }
}