using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Crews.Commands.Disbands;

public class CrewDisbandment(
    ICrewValidationRepository crewValidationRepository,
    ICrewDisbandRepository crewDisbandRepository,
    IUserSession userSession)
    : ICrewDisbandment
{
    public async Task Disband(string crewId)
    {
        var userId = GetUserIdFromSession();
        await CheckIfActiveCrewOwnedByUser(userId);

        await crewDisbandRepository.Disband(crewId);
    }

    private string GetUserIdFromSession()
    {
        var userId = userSession.GetUserId();
        return userId;
    }

    private async Task CheckIfActiveCrewOwnedByUser(string userId)
    {
        var isActiveCrewOwnedByUser = await crewValidationRepository.DoesUserOwnAnActiveCrew(userId);
        
        if (!isActiveCrewOwnedByUser)
            throw new CrewDisbandException();
    }
}