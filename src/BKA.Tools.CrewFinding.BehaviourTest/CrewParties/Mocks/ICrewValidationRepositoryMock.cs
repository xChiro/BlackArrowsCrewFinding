using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public interface ICrewValidationRepositoryMock : ICrewValidationRepository, ICrewQueryRepository
{
    public IReadOnlyList<Crew> StoredCrews { get; }
}