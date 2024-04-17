namespace BKA.Tools.CrewFinding.Players.Creation;

public interface IPlayerCreator
{
    public Task Create(string userId, string citizenName);
}