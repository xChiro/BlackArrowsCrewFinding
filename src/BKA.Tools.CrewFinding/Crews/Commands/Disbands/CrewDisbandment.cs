using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Crews.Commands.Disbands;

public class CrewDisbandment : ICrewDisbandment
{
    private readonly ICrewValidationRepository _crewValidationRepository;
    private readonly ICrewCommandRepository _crewCommandRepository;
    private readonly IUserSession _userSession;

    public CrewDisbandment(ICrewValidationRepository crewValidationRepository, ICrewCommandRepository crewCommandRepository,
        IUserSession userSession)
    {
        _crewValidationRepository = crewValidationRepository;
        _crewCommandRepository = crewCommandRepository;
        _userSession = userSession;
    }

    public async Task Disband(string crewId)
    {
        var userId = GetUserIdFromSession();
        await CheckIfActiveCrewOwnedByUser(userId);

        await _crewCommandRepository.Disband(crewId);
    }

    private string GetUserIdFromSession()
    {
        var userId = _userSession.GetUserId();
        return userId;
    }

    private async Task CheckIfActiveCrewOwnedByUser(string userId)
    {
        var isActiveCrewOwnedByUser = await _crewValidationRepository.DoesUserOwnAnActiveCrew(userId);
        
        if (!isActiveCrewOwnedByUser)
            throw new CrewDisbandException();
    }
}