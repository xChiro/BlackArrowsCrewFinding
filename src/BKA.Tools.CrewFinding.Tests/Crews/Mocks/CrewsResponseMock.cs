using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Queries;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks;

public class CrewsResponseMock : ICrewsResponse
{
    public Crew[]? RecentCrews { get; private set; }

    public void SetRecentCrews(Crew[] recentCrews)
    {
        RecentCrews = recentCrews;
    }
}