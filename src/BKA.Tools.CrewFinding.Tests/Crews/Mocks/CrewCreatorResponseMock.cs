using BKA.Tools.CrewFinding.Crews.Commands.Creators;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks;

public class CrewCreatorResponseMock : ICrewCreatorResponse
{
    public string Id { get; private set;} = string.Empty;

    public void SetResponse(string id)
    {
        Id = id;
    }
}