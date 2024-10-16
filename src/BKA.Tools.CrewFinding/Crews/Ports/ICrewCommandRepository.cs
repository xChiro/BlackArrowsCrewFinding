using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface ICrewCommandRepository
{
    public Task CreateCrew(Crew crew);
    public Task UpdateMembers(string crewId, IEnumerable<Player> crewMembers);
    public Task DeletePlayerHistory(string playerId);
    public Task DeleteCrew(string crewId);
}