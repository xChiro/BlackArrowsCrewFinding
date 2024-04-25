using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface ICrewCommandRepository
{
    public Task CreateCrew(Crew crew);
    public Task UpdateMembers(string crewPartyId, IEnumerable<Player> crewPartyMembers);
    public Task Disband(string crewId);
}