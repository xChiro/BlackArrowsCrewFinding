using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Crews.Commands.Disbands;

public class CrewDisbandment(
    ICrewQueryRepository crewQueryRepository,
    ICrewDisbandRepository crewDisbandRepository,
    IUserSession userSession) : ICrewDisbandment
{
    public async Task Disband()
    {
        var userId = GetUserIdFromSession();
        var crewId = await IsCrewOwnedByUserAsync(userId);

        if (crewId is null)
            throw new CrewDisbandException();

        await crewDisbandRepository.Disband(crewId);
    }

    private string GetUserIdFromSession()
    {
        var userId = userSession.GetUserId();

        return userId;
    }

    private async Task<string?> IsCrewOwnedByUserAsync(string userId)
    {
        var crew = await crewQueryRepository.GetActiveCrewByPlayerId(userId);
        return crew?.Id;
    }
}