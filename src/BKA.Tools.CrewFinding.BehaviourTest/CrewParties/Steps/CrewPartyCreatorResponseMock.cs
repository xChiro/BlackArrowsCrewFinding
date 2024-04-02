using BKA.Tools.CrewFinding.CrewParties.CreateRequests;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

public class CrewPartyCreatorResponseMock : ICrewPartyCreatorResponse
{
    public string Id { get; private set; }
    
    public void SetResponse(string id)
    {
        Id = id;
    }
}