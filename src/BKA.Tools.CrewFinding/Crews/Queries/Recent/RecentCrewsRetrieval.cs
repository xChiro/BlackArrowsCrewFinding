using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Crews.Queries.Recent;

public class RecentCrewsRetrieval(ICrewQueryRepository crewQueryRepository, int crewAgeThresholdInHours)
    : IRecentCrewsRetrieval
{
    public async Task Retrieve(ICrewsResponse response)
    {
        var from = DateTime.UtcNow.AddHours(-crewAgeThresholdInHours);
        var to = DateTime.UtcNow;
        
        var recentCrews = await crewQueryRepository.GetCrews(from, to);
        
        response.SetRecentCrews(recentCrews);
    }
}