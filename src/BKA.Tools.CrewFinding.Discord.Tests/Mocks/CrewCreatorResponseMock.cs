using BKA.Tools.CrewFinding.Crews.Commands.Creators;

namespace BKA.Tools.CrewFinding.Discord.Tests.Mocks;

public class CrewCreatorResponseMock : ICrewCreatorResponse
{
    private string _name = string.Empty;

    public string GetName()
    {
        return _name;
    }

    public void SetResponse(string id, string name)
    {
        _name = name;
    }
}