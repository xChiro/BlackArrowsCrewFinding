using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Players.Creation;

public class PlayerCreator : IPlayerCreator
{
    private readonly IPlayerCommandRepository _playerCommandRepository;
    private readonly int _minLength;
    private readonly int _maxLength;

    public PlayerCreator(IPlayerCommandRepository playerCommandRepository, int minLength, int maxLength)
    {
        _playerCommandRepository = playerCommandRepository;
        _minLength = minLength;
        _maxLength = maxLength;
    }

    public Task Create(string userId, string citizenName)
    {
        var player = Player.Create(userId, citizenName, _minLength, _maxLength);

        return _playerCommandRepository.Create(player);
    }
}