using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Crews.Disbands;

public class CrewDisbandment : ICrewDisbandment
{
    private readonly ICrewQueryRepository _crewQueryRepository;
    private readonly ICrewCommandRepository _crewCommandRepository;
    private readonly IUserSession _userSession;

    public CrewDisbandment(ICrewQueryRepository crewQueryRepository, ICrewCommandRepository crewCommandRepository,
        IUserSession userSession)
    {
        _crewQueryRepository = crewQueryRepository;
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
        var isActiveCrewOwnedByUser = await _crewQueryRepository.IsActiveCrewOwnedBy(userId);
        
        if (!isActiveCrewOwnedByUser)
            throw new CrewDisbandException();
    }
}