using Azure.Core;
using Azure.Core.Pipeline;

namespace BKA.Tools.CrewFinding.KeyVault;

public class KeyVaultProxyPolicy : HttpPipelinePolicy, IDisposable
{
    private readonly Cache _cache;

    public KeyVaultProxyPolicy(TimeSpan? ttl = null)
    {
        ttl ??= TimeSpan.FromHours(1);
        if (ttl < TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(ttl));
        }

        Ttl = ttl.Value;
        _cache = new Cache();
    }

    public TimeSpan Ttl { get; internal set; }

    public void Clear() => _cache.Clear();

    /// <inheritdoc/>
    public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline) =>
#pragma warning disable AZC0102 // TaskExtensions.EnsureCompleted() is not in scope
        ProcessAsync(false, message, pipeline).AsTask().GetAwaiter().GetResult();
#pragma warning restore AZC0102

    /// <inheritdoc/>
    public override async ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline) =>
        await ProcessAsync(true, message, pipeline).ConfigureAwait(false);

    internal static bool IsSupported(string uri)
    {
        // Find the beginning of the path component after the scheme.
        int pos = uri.IndexOf('/', 8);
        if (pos > 0)
        {
            uri = uri.Substring(pos);
            return uri.StartsWith("/secrets/", StringComparison.OrdinalIgnoreCase)
                   || uri.StartsWith("/keys/", StringComparison.OrdinalIgnoreCase)
                   || uri.StartsWith("/certificates/", StringComparison.OrdinalIgnoreCase);
        }

        return false;
    }

    private async ValueTask ProcessAsync(bool isAsync, HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
    {
        Request request = message.Request;
        if (request.Method == RequestMethod.Get)
        {
            string uri = request.Uri.ToUri().GetLeftPart(UriPartial.Path);
            if (IsSupported(uri))
            {
                message.Response = await _cache.GetOrAddAsync(isAsync, uri, Ttl, async () =>
                {
                    await ProcessNextAsync(isAsync, message, pipeline).ConfigureAwait(false);
                    return message.Response;
                }).ConfigureAwait(false);

                return;
            }
        }

        await ProcessNextAsync(isAsync, message, pipeline).ConfigureAwait(false);
    }

    private static async ValueTask ProcessNextAsync(bool isAsync, HttpMessage message,
        ReadOnlyMemory<HttpPipelinePolicy> pipeline)
    {
        if (isAsync)
        {
            await ProcessNextAsync(message, pipeline).ConfigureAwait(false);
        }
        else
        {
            ProcessNext(message, pipeline);
        }
    }

    void IDisposable.Dispose()
    {
        _cache.Dispose();
        GC.SuppressFinalize(this);
    }
}