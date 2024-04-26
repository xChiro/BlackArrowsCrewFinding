using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;
using BKA.Tools.CrewFinding.Crews;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;

public class CrewRepositoriesContext
{
    public CrewCommandRepositoryMock CrewCommandRepositoryMock { get; set; } = new();
    public ICrewValidationRepositoryMock CrewValidationRepositoryMocks { get; set; } = new CrewValidationRepositoryMock(Array.Empty<Crew>());
}