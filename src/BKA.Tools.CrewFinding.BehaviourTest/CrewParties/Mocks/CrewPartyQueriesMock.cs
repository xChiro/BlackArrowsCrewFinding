using BKA.Tools.CrewFinding.CrewParties;
using BKA.Tools.CrewFinding.CrewParties.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewPartyQueriesMock : ICrewPartyQueries
{
    private readonly CrewParty[] _crewParties;
    private readonly bool _playerAlreadyOwnsAParty;
    
    public IReadOnlyList<CrewParty> StoredCrewParties => _crewParties;

    public CrewPartyQueriesMock(bool playerAlreadyOwnsAParty) : this(Array.Empty<CrewParty>(), playerAlreadyOwnsAParty)
    { }

    public CrewPartyQueriesMock(CrewParty[] crewParties, bool playerAlreadyOwnsAParty = false)
    {
        _crewParties = crewParties;
        _playerAlreadyOwnsAParty = playerAlreadyOwnsAParty;
    }

    public Task<bool> PlayerAlreadyInAParty(string captainId)
    {
        return Task.FromResult(_playerAlreadyOwnsAParty);
    }

    public Task<CrewParty?> GetCrewParty(string crewPartyId)
    {
        return Task.FromResult(_crewParties.FirstOrDefault(p => p.Id == crewPartyId));
    }
}