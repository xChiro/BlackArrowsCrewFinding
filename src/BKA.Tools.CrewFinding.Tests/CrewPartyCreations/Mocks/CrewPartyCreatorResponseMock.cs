using BKA.Tools.CrewFinding.CrewParties.CreateRequests;

namespace BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Mocks;

public class CrewPartyCreatorResponseMock : ICrewPartyCreatorResponse
{
    public string Id { get; private set;} = string.Empty;
    
    public void SetResponse(string id)
    {
        Id = id;
    }

}