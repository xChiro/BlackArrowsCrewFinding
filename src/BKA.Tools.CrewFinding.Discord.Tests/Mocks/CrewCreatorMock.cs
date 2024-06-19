using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Discord.Tests.Mocks;

public class CrewCreatorMock(string captainName) : ICrewCreator
{
    public Task Create(CrewCreatorRequest request, ICrewCreatorResponse output)
    {
        var crew = request.ToCrew(Player.Create("1", captainName, 2, 30), 4);
        output.SetResponse("1", crew.Name.Value);

        return Task.CompletedTask;
    }
}