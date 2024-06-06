using BKA.Tools.CrewFinding.Crews.Commands.Expired;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks;

public class ExpiredCrewRemoverResponseMock : IExpiredCrewRemoverResponse
{
    public string[] CrewIds { get; private set; } = [];

    public void RemovedCrews(string[] crewIds)
    {
        CrewIds = crewIds;
    }
}