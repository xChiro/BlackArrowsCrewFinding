using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Crews.Commands.Leave;

public class CrewLeaver(ICrewQueryRepository crewQueriesRepository,  ICrewCommandRepository crewCommandMock,
    IUserSession userSession) : ICrewLeaver
{
    public async Task Leave()
    {
        var playerId = userSession.GetUserId();
        var crew = await crewQueriesRepository.GetActiveCrewByPlayerId(playerId);
        
        if (crew == null || !crew.RemoveMember(playerId))
            throw new NoCrewMemberException();

        await crewCommandMock.UpdateMembers(crew.Id, crew.Members);
    }
}