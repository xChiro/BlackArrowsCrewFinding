using BKA.Tools.CrewFinding.Crews.Commands.Creators;

namespace BKA.Tools.CrewFinding.Discord.Tests.Mocks;

public class CrewCreatorExceptionMock : ICrewCreator
{
    public Task Create(CrewCreatorRequest request, ICrewCreatorResponse output)
    {
        throw new NotImplementedException();
    }
}