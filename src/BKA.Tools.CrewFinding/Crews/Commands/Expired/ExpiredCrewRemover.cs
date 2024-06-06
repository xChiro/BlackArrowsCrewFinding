using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Crews.Commands.Expired;

public class ExpiredCrewRemover(
    ICrewQueryRepository crewQueryRepositoryMock,
    ICrewDisbandRepository crewCommandRepositoryMock,
    int expirationThreshold)
    : IExpiredCrewRemover
{
    public async Task Remove()
    {
        var crews = await crewQueryRepositoryMock.GetCrewsExpiredByDate(DateTime.UtcNow.AddHours(-expirationThreshold));
        
        if (crews.Length != 0)
        {
            var crewIds = crews.Select(c => c.Id).ToArray();
            await crewCommandRepositoryMock.Disband(crewIds);
        }
    }
}