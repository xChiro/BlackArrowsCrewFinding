using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure;
using Azure.Core;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings.KeyVault;

internal class CachedResponse : Response
{
    private readonly ResponseHeaders _headers;
    private DateTimeOffset _expires;

    private CachedResponse(int status, string reasonPhrase, ResponseHeaders headers)
    {
        Status = status;
        ReasonPhrase = reasonPhrase;

        _headers = headers;
    }

    public override int Status { get; }

    public override string ReasonPhrase { get; }

    public override Stream ContentStream { get; set; }

    public override string ClientRequestId { get; set; }

    internal bool IsValid => DateTimeOffset.Now <= _expires;

    public override void Dispose() => ContentStream?.Dispose();

    internal static async ValueTask<CachedResponse> CreateAsync(bool isAsync, Response response, TimeSpan ttl)
    {
        CachedResponse cachedResponse = await CloneAsync(isAsync, response).ConfigureAwait(false);
        cachedResponse._expires = DateTimeOffset.Now + ttl;

        return cachedResponse;
    }

    internal async ValueTask<Response> CloneAsync(bool isAsync) =>
        await CloneAsync(isAsync, this).ConfigureAwait(false);

    protected override bool ContainsHeader(string name) => _headers.Contains(name);
    protected override IEnumerable<HttpHeader> EnumerateHeaders() => _headers;
    protected override bool TryGetHeader(string name, out string value) => _headers.TryGetValue(name, out value);

    protected override bool TryGetHeaderValues(string name, out IEnumerable<string> values) =>
        _headers.TryGetValues(name, out values);

    private static async ValueTask<CachedResponse> CloneAsync(bool isAsync, Response response)
    {
        CachedResponse cachedResponse = new CachedResponse(response.Status, response.ReasonPhrase, response.Headers)
        {
            ClientRequestId = response.ClientRequestId,
        };

        if (response.ContentStream is { })
        {
            MemoryStream ms = new MemoryStream();
            cachedResponse.ContentStream = ms;

            if (isAsync)
            {
                await response.ContentStream.CopyToAsync(cachedResponse.ContentStream).ConfigureAwait(false);
            }
            else
            {
                response.ContentStream.CopyTo(cachedResponse.ContentStream);
            }

            ms.Position = 0;
            if (response.ContentStream.CanSeek)
            {
                response.ContentStream.Position = 0;
            }
            else
            {
                response.ContentStream = new MemoryStream(ms.ToArray());
            }
        }

        return cachedResponse;
    }
}