using System;
using BKA.Tools.CrewFinding.Channels.Expired;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BKA.Tools.CrewFinding.API.Functions.Channels.Expired;

public class RemoveExpiredChannelFunction(IExpiredChannelRemover expiredChannelRemover, ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<RemoveExpiredChannelFunction>();

    [Function("RemoveExpiredChannelFunction")]
    public void Run([TimerTrigger("0 0 */4 * * *")] TimerInfo myTimer)
    {
        try
        {
            expiredChannelRemover.Remove();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to remove expired channels");
        }
    }
}