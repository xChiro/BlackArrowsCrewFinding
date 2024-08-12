using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Profiles;

public class ProfileViewerSignalR(
    IProfileViewer decorated,
    ISignalRGroupService signalRGroupService,
    IDomainLogger domainLogger) : IProfileViewer
{
    public async Task View(string playerId, IProfileResponse response)
    {
        var internalResponse = new ProfileViewerResponse();
        await decorated.View(playerId, internalResponse);

        response.SetResponse(internalResponse.Player!, internalResponse.ActiveCrewId, internalResponse.ActiveCrewName);

        if (internalResponse.ActiveCrewId is not "")
        {
            try
            {
                signalRGroupService.AddUserToGroupAsync(playerId, internalResponse.ActiveCrewId);
            }
            catch (Exception e)
            {
                domainLogger.Log(e,
                    $"Error join to SignalR group with crewId: {internalResponse.ActiveCrewId}, playerId: {playerId}");
            }
        }
    }
}