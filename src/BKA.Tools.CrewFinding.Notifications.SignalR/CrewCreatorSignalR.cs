using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Crews.Ports;
using Microsoft.AspNetCore.SignalR;

namespace BKA.Tools.CrewFinding.Notifications.SignalR;

public class CrewCreatorSignalR(
    ICrewCreator crewCreator,
    IHubContext<CrewHub> crewHubContext,
    IUserSession userSession,
    IDomainLogger logger)
    : ICrewCreator, ICrewCreatorResponse
{
    private string _id = string.Empty;
    private string _name = string.Empty;

    public async Task Create(ICrewCreatorRequest request, ICrewCreatorResponse output)
    {
        await crewCreator.Create(request, this);
        output.SetResponse(_id, _name);

        _ = Task.Run(async () =>
        {
            try
            {
                await crewHubContext.Groups.AddToGroupAsync(userSession.GetUserId(), _id);
            }
            catch (Exception e)
            {
                logger.Log(e, "Error adding captain to SignalR group");
            }
        });
    }

    public void SetResponse(string id, string name)
    {
        _id = id;
        _name = name;
    }
}