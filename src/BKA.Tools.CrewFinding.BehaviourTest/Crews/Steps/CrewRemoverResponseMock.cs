using BKA.Tools.CrewFinding.Crews.Commands.Expired;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

public class CrewRemoverResponseMock : ICrewRemoverResponse
{
    public string[] CrewIds { get; private set; } = [];

    public void RemovedCrews(string[] crewIds)
    {
        CrewIds = crewIds;
    }
}