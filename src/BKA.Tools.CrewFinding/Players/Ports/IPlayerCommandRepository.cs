namespace BKA.Tools.CrewFinding.Players.Ports;

public interface IPlayerCommandRepository
{
    public Task Create(Player player);
}