namespace BKA.Tools.CrewFinding.Crews.Commands.Expired;

public interface IExpiredCrewRemoverResponse
{
    public void RemovedCrews(string[] crewIds);
}