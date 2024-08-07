using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Notifications.SignalR;

public class CrewCreatorSignalR(
    ICrewCreator crewCreator,
    ISignalRGroupService signalRGroupService,
    ISignalRUserSession userSession,
    IDomainLogger logger)
    : ICrewCreator, ICrewCreatorResponse
{
    private string _id = string.Empty;
    private string _name = string.Empty;

    public async Task Create(ICrewCreatorRequest request, ICrewCreatorResponse output)
    {
        await crewCreator.Create(request, this);
        output.SetResponse(_id, _name);

        try
        {
            signalRGroupService.AddUserToGroupAsync(userSession.GetConnectionId(), _id);
        }
        catch (Exception e)
        {
            logger.Log(e, $"Error adding captain {userSession.GetUserId()} to SignalR group {_id}");
        }
    }

    public void SetResponse(string id, string name)
    {
        _id = id;
        _name = name;
    }
}