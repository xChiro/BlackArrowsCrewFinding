using BKA.Tools.CrewFinding.Crews.Commands.Creators;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.Crews;

public class CrewCreatorExceptionMock<T> : ICrewCreator where T : Exception, new()
{
    public Task Create(ICrewCreatorRequest request, ICrewCreatorResponse output)
    {
        throw new T();
    }
}