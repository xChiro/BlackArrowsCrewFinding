using BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;

public class CrewRepositoriesContext
{
    public CrewCommandRepositoryMock CrewCommandRepositoryMock { get; set; } = new();
    public ICrewValidationRepository CrewValidationRepositoryMocks { get; set; } = new CrewValidationRepositoryMock();
    public ICrewQueryRepositryMock CrewQueryRepositoryMock { get; set; } = new CrewQueryRepositoryMock(Array.Empty<Crew>());
}