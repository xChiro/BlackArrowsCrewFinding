using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Queries;
using BKA.Tools.CrewFinding.Tests.Crews.Queries;
using BKA.Tools.CrewFinding.Tests.Crews.Queries.Recents;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks;

public class CrewsResponseMock : ICrewsResponse
{
    public Crew[]? RecentCrews { get; private set; }

    public void SetRecentCrews(Crew[] recentCrews)
    {
        RecentCrews = recentCrews;
    }
}