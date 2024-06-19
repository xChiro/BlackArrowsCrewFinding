using BKA.Tools.CrewFinding.Crews.Commands.Creators;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks;

public class CrewCreatorResponseMock : ICrewCreatorResponse
{
    private string _name = string.Empty;
    public string Id { get; private set;} = string.Empty;
    

    public string GetName()
    {
        return _name;
    }

    public void SetResponse(string id, string name)
    {
        Id = id;
        _name = name;
    }
}