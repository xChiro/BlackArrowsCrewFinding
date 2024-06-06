using BKA.Tools.CrewFinding.Crews.Commands.Expired;

namespace BKA.Tools.CrewFinding.API.Functions.Commands.RemoveExpired;

public class ExpiredCrewRemoverFunction(IExpiredCrewRemover expiredCrewRemover, ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<ExpiredCrewRemoverFunction>();

    [Function("ExpiredCrewRemoverFunction")]
    public void Run([TimerTrigger("0 0 */4 * * *")] TimerInfo myTimer)
    {
        try
        {
            expiredCrewRemover.Remove();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while removing expired crews.");
        }
    }
}