using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;
using BKA.Tools.CrewFinding.Crews;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;

public class CrewRepositoriesContext
{
    public CrewCommandRepositoryMock CrewCommandRepositoryMock { get; set; } = new();
    public ICrewQueryRepositoryMock CrewQueryRepositoryMocks { get; set; } = new CrewQueryRepositoryMock(Array.Empty<Crew>());
}