using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Crews;

public class CrewDisbandmentSignalR(
    ICrewDisbandment decorated,
    ISignalRGroupService signalRGroupService,
    IUserSession userSession,
    IDomainLogger domainLogger) : ICrewDisbandment
{
    public async Task Disband(ICrewDisbandmentResponse output)
    {
        var crewDisbandmentResponse = new CrewDisbandmentResponse();
        await decorated.Disband(crewDisbandmentResponse);
        output.SetResult(crewDisbandmentResponse.CrewId);

        try
        {
            signalRGroupService.RemoveAllFromGroupAsync(crewDisbandmentResponse.CrewId);
            signalRGroupService.SendMessageToGroupAsync(crewDisbandmentResponse.CrewId, new
            {
                message = "The captain disbanded the crew."
            }, "CrewDisbanded", [userSession.GetUserId()]);
        }
        catch (Exception e)
        {
            domainLogger.Log(e, "Error disbanding crew");
        }
    }
}