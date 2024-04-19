using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewQueriesMock : ICrewQueries
{
    private readonly Crew[] _crewParties;
    private readonly bool _playerAlreadyOwnsAParty;
    
    public IReadOnlyList<Crew> StoredCrewParties => _crewParties;

    public CrewQueriesMock(bool playerAlreadyOwnsAParty) : this(Array.Empty<Crew>(), playerAlreadyOwnsAParty)
    { }

    public CrewQueriesMock(Crew[] crewParties, bool playerAlreadyOwnsAParty = false)
    {
        _crewParties = crewParties;
        _playerAlreadyOwnsAParty = playerAlreadyOwnsAParty;
    }

    public Task<bool> PlayerAlreadyInACrew(string captainId)
    {
        return Task.FromResult(_playerAlreadyOwnsAParty);
    }

    public Task<Crew?> GetCrewParty(string crewPartyId)
    {
        return Task.FromResult(_crewParties.FirstOrDefault(p => p.Id == crewPartyId));
    }
}