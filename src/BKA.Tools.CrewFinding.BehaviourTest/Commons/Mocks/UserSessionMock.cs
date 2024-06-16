using BKA.Tools.CrewFinding.Commons.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.Commons.Mocks;

public class UserSessionMock(string playerId) : IUserSession
{
    public string GetUserId()
    {
        return playerId;
    }
}