using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.CrewParties.Ports;

public interface ICrewPartyCommands
{
    public Task CreateCrewParty(CrewParty crewParty);
    public Task UpdateMembers(string crewPartyId, IEnumerable<Player> crewPartyMembers);
}