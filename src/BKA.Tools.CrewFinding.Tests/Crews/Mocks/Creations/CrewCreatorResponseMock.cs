using BKA.Tools.CrewFinding.Crews.Commands.Creators;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.Creations;

public class CrewCreatorResponseMock : ICrewCreatorResponse
{
    public string Id { get; private set;} = string.Empty;
    public string Name { get; private set;  }

    public void SetResponse(string id, string name)
    {
        Id = id;
        Name = name;
    }
}