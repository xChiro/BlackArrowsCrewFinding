using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;

public class CrewCommandRepositoryMock : ICrewCommandRepository, ICrewDisbandRepository
{
    private Crew? _crewPartyCreated;

    public Player[] CrewPartyMembers { get; private set; } = [];
    public string[] DisbandedCrewIds { get; set; } = [];

    public Task CreateCrew(Crew crew)
    {
        _crewPartyCreated = crew;
        return Task.CompletedTask;
    }

    public Task UpdateMembers(string crewId, IEnumerable<Player> crewMembers)
    {
        CrewPartyMembers = crewMembers.ToArray();
        return Task.CompletedTask;
    }

    public Task DeletePlayerHistory(string playerId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCrew(string crewId)
    {
        throw new NotImplementedException();
    }

    public Task Disband(string crewId)
    {
        DisbandedCrewIds = [crewId];
        return Task.CompletedTask;
    }

    public Task Disband(string[] crewIds)
    {
        DisbandedCrewIds = crewIds;
        return Task.CompletedTask;
    }

    public Player? GetCaptain()
    {
        return _crewPartyCreated?.Captain;
    }

    public Crew? GetStoredCrew()
    {
        return _crewPartyCreated;
    }
}