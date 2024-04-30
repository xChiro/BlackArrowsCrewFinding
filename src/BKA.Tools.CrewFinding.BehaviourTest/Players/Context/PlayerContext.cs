using BKA.Tools.CrewFinding.BehaviourTest.Commons.Mocks;
using BKA.Tools.CrewFinding.Commons.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Context;

public class PlayerContext
{
    public string PlayerName { get; set; } = "Rowan";
    public string PlayerId { get; set; }
    public int MaxLength { get; set; } = 30;
    public int MinLength { get; set; } = 2;
    
    public IUserSession CreateUserSession()
    {
        return new UserSessionMock(PlayerId);
    }
}