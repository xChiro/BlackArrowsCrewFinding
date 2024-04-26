using BKA.Tools.CrewFinding.Crews.Commands.CreateRequests;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;

public class CrewCreatorResponseMock : ICrewCreatorResponse
{
    public string Id { get; private set; }
    
    public void SetResponse(string id)
    {
        Id = id;
    }
}