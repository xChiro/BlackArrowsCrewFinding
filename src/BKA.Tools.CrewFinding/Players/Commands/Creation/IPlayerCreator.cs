namespace BKA.Tools.CrewFinding.Players.Commands.Creation;

public interface IPlayerCreator
{
    public Task Create(string userId, string citizenName);
}