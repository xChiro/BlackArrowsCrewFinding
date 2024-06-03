using BKA.Tools.CrewFinding.API.Functions.Queries.RecentCrews;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Tests.Crews.Queries.Recent;

namespace BKA.Tools.CrewFinding.API.Functions.Queries.GetCrew;

public class GetCrewResponse : CrewResponse, ICrewResponse
{
    public void SetCrew(Crew crew)
    {
        UpdateFrom(crew);
    }
}