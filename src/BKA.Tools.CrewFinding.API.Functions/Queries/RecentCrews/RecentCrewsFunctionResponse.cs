using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Queries;

namespace BKA.Tools.CrewFinding.API.Functions.Queries.RecentCrews;

public class RecentCrewsFunctionResponse : ICrewsResponse
{
    public Crew[] Crews { get; private set; }
    
    public void SetRecentCrews(Crew[] recentCrews)
    {
        Crews = recentCrews;
    }
}