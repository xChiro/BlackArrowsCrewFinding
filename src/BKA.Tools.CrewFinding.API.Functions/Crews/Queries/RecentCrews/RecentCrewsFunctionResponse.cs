using BKA.Tools.CrewFinding.API.Functions.Models;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Queries;

namespace BKA.Tools.CrewFinding.API.Functions.Crews.Queries.RecentCrews;

public class RecentCrewsFunctionResponse : ICrewsResponse
{
    public CrewResponse[] Crews { get; private set; } = [];
    
    public void SetRecentCrews(Crew[] recentCrews)
    {
        Crews = recentCrews.Select(CrewResponse.CreateFrom).ToArray();
    }
}