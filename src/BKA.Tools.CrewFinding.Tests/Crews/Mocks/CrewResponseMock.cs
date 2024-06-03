using BKA.Tools.CrewFinding.Crews;

namespace BKA.Tools.CrewFinding.Tests.Crews.Queries.Recent;

public class CrewResponseMock : ICrewResponse
{
    public Crew? Crew { get; private set; }

    public void SetCrew(Crew crew)
    {
        Crew = crew;
    }
}