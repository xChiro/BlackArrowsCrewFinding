using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Crews.Commands.Disbands;

public class CrewDisbandment(
    ICrewQueryRepository crewQueryRepository,
    ICrewDisbandRepository crewDisbandRepository,
    IUserSession userSession) : ICrewDisbandment
{
    public async Task Disband(ICrewDisbandmentResponse output)
    {
        var userId = GetUserIdFromSession();
        var crew = await IsCrewOwnedByUserAsync(userId);

        if (crew is null)
            throw new CrewDisbandException();

        await crewDisbandRepository.Disband(crew.Id);
        
        output.SetResult(crew.Id);
    }

    private string GetUserIdFromSession()
    {
        var userId = userSession.GetUserId();
        return userId;
    }

    private async Task<Crew?> IsCrewOwnedByUserAsync(string userId)
    {
        return  await crewQueryRepository.GetActiveCrewByPlayerId(userId);
    }
}