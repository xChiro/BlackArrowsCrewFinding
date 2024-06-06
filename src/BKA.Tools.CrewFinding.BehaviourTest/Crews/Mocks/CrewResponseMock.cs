using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Queries;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;

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