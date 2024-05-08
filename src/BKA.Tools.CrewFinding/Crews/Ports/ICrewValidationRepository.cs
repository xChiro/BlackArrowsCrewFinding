namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface ICrewValidationRepository
{
    public Task<bool> IsPlayerInActiveCrew(string playerId);
}