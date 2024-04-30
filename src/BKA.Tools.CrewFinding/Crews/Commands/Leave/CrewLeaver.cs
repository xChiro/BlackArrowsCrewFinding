using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Crews.Commands.Leave;

public class CrewLeaver( ICrewQueryRepository crewQueriesRepository,  ICrewCommandRepository crewCommandMock,
    IUserSession userSession)  : ICrewLeaver
{
    public async Task Leave(string crewId)
    {
        var crew = await crewQueriesRepository.GetCrew(crewId);

        if (crew == null)
            throw new CrewNotFoundException(crewId);
        
        var playerId = userSession.GetUserId();
        if (!crew.LeaveMember(playerId))
            throw new PlayerNotInCrewException();

        await crewCommandMock.UpdateMembers(crewId, crew.Members);
    }
}