using BKA.Tools.CrewFinding.Crews.CreateRequests;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewCreatorResponseMock : ICrewCreatorResponse
{
    public string Id { get; private set; }
    
    public void SetResponse(string id)
    {
        Id = id;
    }
}