using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;

public interface ICrewQueryRepositryMock : ICrewQueryRepository
{
    public IReadOnlyList<Crew> StoredCrews { get; }
}