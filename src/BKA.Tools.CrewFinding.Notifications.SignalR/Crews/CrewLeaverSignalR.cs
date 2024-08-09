using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Commands.Leave;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Crews;

public class CrewLeaverSignalR(
    ICrewLeaver decorated,
    IDomainLogger domainLogger,
    ISignalRGroupService signalRGroupService,
    IUserSession userSession) : ICrewLeaver, ICrewLeaverResponse
{
    private string _crewId = string.Empty;

    public async Task Leave(ICrewLeaverResponse output)
    {
        await decorated.Leave(this);
        output.SetResponse(_crewId);

        try
        {
            signalRGroupService.RemoveUserFromGroupAsync(userSession.GetUserId(), _crewId);
            signalRGroupService.SendMessageToGroupAsync(_crewId, new
            {
                PlayerId = userSession.GetUserId(),
                message = "Player left the crew"
            }, "PlayerLeftCrew");
        }
        catch (Exception e)
        {
            domainLogger.Log(e, "Error leaving crew");
        }
    }

    public void SetResponse(string crewId)
    {
        _crewId = crewId;
    }
}