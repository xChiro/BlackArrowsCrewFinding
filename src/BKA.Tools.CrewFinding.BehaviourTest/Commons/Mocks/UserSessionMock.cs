using BKA.Tools.CrewFinding.Commons.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.Commons.Mocks;

public class UserSessionMock : IUserSession
{
    private readonly string _playerId;

    public UserSessionMock(string playerId)
    {
        _playerId = playerId;
    }

    public string GetUserId()
    {
        return _playerId;
    }
}