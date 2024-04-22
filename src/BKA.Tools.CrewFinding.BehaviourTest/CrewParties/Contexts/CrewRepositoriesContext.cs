using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;
using BKA.Tools.CrewFinding.Crews;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;

public class CrewRepositoriesContext
{
    public CrewCommandRepositoryMock CrewCommandRepositoryMock { get; set; } = new();
    public ICrewQueriesMock CrewQueriesMocks { get; set; } = new CrewQueriesMock(Array.Empty<Crew>());
}