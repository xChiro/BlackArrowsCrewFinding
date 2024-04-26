using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Queries;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;

public class CrewQueryResultContext : ICrewsResponse
{
    public Crew[] Crews { get; set; }

    public void SetRecentCrews(Crew[] recentCrews)
    {
        Crews = recentCrews;
    }
}