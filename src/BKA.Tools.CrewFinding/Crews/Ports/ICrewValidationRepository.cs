namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface ICrewQueryRepository
{
    Task<Crew?> GetCrew(string crewId);
    Task<Crew[]> GetCrews();
}

public interface ICrewValidationRepository
{
    public Task<bool> IsPlayerInActiveCrew(string playerId);

    Task<bool> IsActiveCrewOwnedBy(string crewId);
}