using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.API.Functions;

public class DomainLogger(ILogger logger) : IDomainLogger
{
    public void Log(Exception e, string message)
    {
        logger.LogError(e, message);
    }
}