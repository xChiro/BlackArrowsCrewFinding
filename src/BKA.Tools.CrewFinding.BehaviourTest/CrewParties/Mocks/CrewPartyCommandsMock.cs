using BKA.Tools.CrewFinding.CrewParties;
using BKA.Tools.CrewFinding.CrewParties.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewPartyCommandsMock : ICrewPartyCommandsMock
{
    private Captain? _captain;
    private CrewParty? _crewParty;

    public void SaveCrewParty(Captain captain, CrewParty crewParty)
    {
        _captain = captain;
        _crewParty = crewParty;
    }

    public Captain? GetCaptain()
    {
        return _captain;
    }

    public CrewParty? GetCrewParty()
    {
        return _crewParty;
    }
}

public interface ICrewPartyCommandsMock : ICrewPartyCommands
{
    public Captain? GetCaptain();

    public CrewParty? GetCrewParty();
}