using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewCommandsMock : ICrewCommands
{
    private Crew? _crewPartyCreated;

    public Player[] CrewPartyMembers { get; private set; } = Array.Empty<Player>();

    public Task CreateCrew(Crew crew)
    {
        _crewPartyCreated = crew;

        return Task.CompletedTask;
    }

    public Task UpdateMembers(string crewPartyId, IEnumerable<Player> crewPartyMembers)
    {
        CrewPartyMembers = crewPartyMembers.ToArray();
        
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