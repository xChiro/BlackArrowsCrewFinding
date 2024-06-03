using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Tests.Crews.Queries.Recent;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

public class CrewResponseMock : ICrewResponse
{
    public Crew? Crew { get; private set; }

    public Exception? ExceptionResult { get; set; }

    public void SetCrew(Crew crew)
    {
        Crew = crew;
    }

    public void SetException(Exception exception)
    {
        ExceptionResult = exception;
    }
}