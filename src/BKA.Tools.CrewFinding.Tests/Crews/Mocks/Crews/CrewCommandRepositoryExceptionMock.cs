using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;

public class CrewCommandRepositoryExceptionMock<T> : ICrewCommandRepository where T : Exception, new()
{
    public Task CreateCrew(Crew crew)
    {
        throw new T();
    }

    public Task UpdateMembers(string crewId, IEnumerable<Player> crewMembers)
    {
        throw new T();
    }

    public Task DeletePlayerHistory(string playerId)
    {
        throw new T();
    }

    public Task DeleteCrew(string crewId)
    {
        throw new T();
    }
}