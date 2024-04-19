using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface ICrewCommands
{
    public Task CreateCrew(Crew crew);
    public Task UpdateMembers(string crewPartyId, IEnumerable<Player> crewPartyMembers);
}