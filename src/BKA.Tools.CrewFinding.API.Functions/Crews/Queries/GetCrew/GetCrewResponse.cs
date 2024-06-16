using BKA.Tools.CrewFinding.API.Functions.Models;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Queries;

namespace BKA.Tools.CrewFinding.API.Functions.Crews.Queries.GetCrew;

public class GetCrewResponse : CrewResponse, ICrewResponse
{
    public void SetCrew(Crew crew)
    {
        UpdateFrom(crew);
    }
}