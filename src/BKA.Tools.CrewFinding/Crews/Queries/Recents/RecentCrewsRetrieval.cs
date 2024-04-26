using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Crews.Queries.Recents;

public class RecentCrewsRetrieval : IRecentCrewsRetrieval
{
    private readonly ICrewQueryRepository _crewQueryRepository;
    private readonly int _crewAgeThresholdInHours;

    public RecentCrewsRetrieval(ICrewQueryRepository crewQueryRepository, int crewAgeThresholdInHours)
    {
        _crewQueryRepository = crewQueryRepository;
        _crewAgeThresholdInHours = crewAgeThresholdInHours;
    }

    public async Task Retrieve(ICrewsResponse response)
    {
        var from = DateTime.UtcNow.AddHours(-_crewAgeThresholdInHours);
        var to = DateTime.UtcNow;
        
        var recentCrews = await _crewQueryRepository.GetCrews(from, to);
        
        response.SetRecentCrews(recentCrews);
    }
}