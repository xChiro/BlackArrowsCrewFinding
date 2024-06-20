using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Crews.Commands.Expired;

public class ExpiredCrewRemover(
    ICrewQueryRepository crewQueryRepository,
    ICrewDisbandRepository crewDisbandRepository,
    int expirationThreshold)
    : IExpiredCrewRemover
{
    public async Task Remove()
    {
        var crews = await crewQueryRepository.GetCrewsExpiredByDate(DateTime.UtcNow.AddHours(-expirationThreshold));
        var crewIds = crews.Select(c => c.Id).ToArray();
        
        await crewDisbandRepository.Disband(crewIds);
    }
}