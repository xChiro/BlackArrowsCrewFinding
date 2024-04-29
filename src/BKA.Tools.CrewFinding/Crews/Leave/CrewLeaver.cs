using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Tests.Crews.Commands.Leave;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Leave;

public class CrewLeaver : ICrewLeaver
{
    private readonly ICrewQueryRepository _crewQueriesRepository;
    private readonly ICrewCommandRepository _crewCommandMock;

    public CrewLeaver(ICrewQueryRepository crewQueriesRepository, ICrewCommandRepository crewCommandMock)
    {
        _crewQueriesRepository = crewQueriesRepository;
        _crewCommandMock = crewCommandMock;
    }

    public async Task Leave(string playerId, string crewId)
    {
        var crew = await _crewQueriesRepository.GetCrew(crewId);
        
        if(crew == null)
            throw new CrewNotFoundException(crewId);
        
        if(!crew.LeaveMember(playerId))
            throw new PlayerNotInCrewException();

        await _crewCommandMock.UpdateMembers(crewId, crew.Members);
    }
}