using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public interface ICrewQueryRepositoryMock : ICrewQueryRepository
{
    public IReadOnlyList<Crew> StoredCrews { get; }
}