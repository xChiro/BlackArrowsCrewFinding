using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Players.Commands.Creation;

public class PlayerCreator(IPlayerCommandRepository playerCommandRepository, int minLength, int maxLength)
    : IPlayerCreator
{
    public Task Create(string userId, string citizenName)
    {
        var player = Player.Create(userId, citizenName, minLength: minLength, maxLength: maxLength);

        return playerCommandRepository.Create(player);
    }
}