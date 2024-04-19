using BKA.Tools.CrewFinding.Crews.CreateRequests;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

public class CrewCreatorResponseMock : ICrewCreatorResponse
{
    public string Id { get; private set;} = string.Empty;
    
    public void SetResponse(string id)
    {
        Id = id;
    }

}