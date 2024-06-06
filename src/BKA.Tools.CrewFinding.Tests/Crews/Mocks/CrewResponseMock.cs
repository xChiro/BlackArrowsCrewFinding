using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Queries;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks;

public class CrewResponseMock : ICrewResponse
{
    public Crew? Crew { get; private set; }

    public void SetCrew(Crew crew)
    {
        Crew = crew;
    }
}