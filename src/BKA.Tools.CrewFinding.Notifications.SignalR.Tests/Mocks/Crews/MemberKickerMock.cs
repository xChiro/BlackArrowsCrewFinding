using BKA.Tools.CrewFinding.Crews.Commands.Kicks;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.Crews;

public class MemberKickerMock(string crewId = "1345") : IMemberKicker
{
    public string KickedMemberId { get; private set; }

    public Task Kick(string memberId, IMemberKickerResponse output)
    {
        KickedMemberId = memberId;
        output.SetResponse(crewId);
        return Task.CompletedTask;
    }
}