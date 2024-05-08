using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Crews.Commands.Disbands;

public class CrewDisbandment(
    ICrewQueryRepository crewQueryRepository,
    ICrewDisbandRepository crewDisbandRepository,
    IUserSession userSession)
    : ICrewDisbandment
{
    public async Task Disband(string crewId)
    {
        var userId = GetUserIdFromSession();

        if (!await IsCrewOwnedByUserAsync(userId, crewId))
            throw new CrewDisbandException();

        await crewDisbandRepository.Disband(crewId);
    }

    private string GetUserIdFromSession()
    {
        var userId = userSession.GetUserId();

        return userId;
    }

    private async Task<bool> IsCrewOwnedByUserAsync(string userId, string crewId)
    {
        var crew = await crewQueryRepository.GetCrew(crewId);

        return crew?.Captain.Id == userId;
    }
}