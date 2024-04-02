using BKA.Tools.CrewFinding.CrewParties;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewPartyCommandsMock : ICrewPartyCommands
{
    private Player? _captain;
    private CrewParty? _crewParty;

    public Task<string> SaveCrewParty(Player captain, CrewParty crewParty)
    {
        _captain = captain;
        _crewParty = crewParty;

        return Task.FromResult(Guid.NewGuid().ToString());
    }

    public Task AddPlayerToCrewParty(string playerId, string crewPartyId)
    {
        throw new NotImplementedException();
    }

    public Player? GetCaptain()
    {
        return _captain;
    }

    public CrewParty? GetCrewParty()
    {
        return _crewParty;
    }
}