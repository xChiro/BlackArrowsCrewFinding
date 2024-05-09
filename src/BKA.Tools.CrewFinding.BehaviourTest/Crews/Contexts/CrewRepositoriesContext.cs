using BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;

public class CrewRepositoriesContext
{
    public CrewCommandRepositoryMock CommandRepositoryMock { get; set; } = new();
    public ICrewValidationRepository ValidationRepositoryMocks { get; set; } = new CrewValidationRepositoryMock();
    public ICrewQueryRepositryMock QueryRepositoryMock { get; set; } = new CrewQueryRepositoryMock([]);
}